#region About
// Based (largely on) code by: Ianier Munoz | ianier@hotmail.com
// http://www.codeproject.com/KB/audio-video/cswavrec.aspx?fid=16677&fr=276#xx0xx
// for original licence refer to his code
// msdn article: Creating a PInvoke library in C# - http://msdn.microsoft.com/en-us/library/aa446550.aspx

// ToDo:
// format - done
// migrate wavenative (we don't need headers in c#) - done
// add record, pause, stop, resume, device count, device desc - done
// fix threading issues - done
// add properties - done
// add error handling - done
// comment - done
#endregion

#region Directives
using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
#endregion

namespace WaveLib
{
    #region Public Delegates
    public delegate void BufferDoneEventHandler(IntPtr data, uint size);
    #endregion

    internal class WaveInBuffer : IDisposable
    {
        #region Constants
        public const int MM_WOM_OPEN = 0x3BB;
        public const int MM_WOM_CLOSE = 0x3BC;
        public const int MM_WOM_DONE = 0x3BD;
        public const int MM_WIM_OPEN = 0x3BE;
        public const int MM_WIM_CLOSE = 0x3BF;
        public const int MM_WIM_DATA = 0x3C0;
        #endregion

        #region API
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR lpWaveInHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInAddBuffer(IntPtr hwi, ref WAVEHDR pwh, int cbwh);
        #endregion

        #region Fields
        private bool _bRecording;
		private IntPtr _pWaveIn;
		private byte[] _cHeaderData;
        public WaveInBuffer _cNextBuffer;
		private AutoResetEvent _cRecordEvent = new AutoResetEvent(false);
		private WAVEHDR _cHeader;
		private GCHandle _hHeaderHandle;
		private GCHandle _hHeaderDataHandle;
        #endregion

        #region Constructor
        public WaveInBuffer(IntPtr waveInHandle, uint size)
        {
            _pWaveIn = waveInHandle;
            _hHeaderHandle = GCHandle.Alloc(_cHeader, GCHandleType.Pinned);
            _cHeader.dwUser = (IntPtr)GCHandle.Alloc(this);
            _cHeaderData = new byte[size];
            _hHeaderDataHandle = GCHandle.Alloc(_cHeaderData, GCHandleType.Pinned);
            _cHeader.lpData = _hHeaderDataHandle.AddrOfPinnedObject();
            _cHeader.dwBufferLength = size;
            waveInPrepareHeader(_pWaveIn, ref _cHeader, Marshal.SizeOf(_cHeader));
        }
        ~WaveInBuffer()
        {
            Dispose();
        }
        #endregion

        #region Callback
        internal static void WaveInProc(IntPtr hdrvr, int uMsg, int dwUser, ref WAVEHDR wavhdr, int dwParam2)
		{
			if (uMsg == MM_WIM_DATA)
			{
				try
				{
					GCHandle h = (GCHandle)wavhdr.dwUser;
					WaveInBuffer buf = (WaveInBuffer)h.Target;
					buf.OnCompleted();
				}
				catch { }
			}
        }
        #endregion

        #region Properties
        public uint Size
		{
			get { return _cHeader.dwBufferLength; }
		}

		public IntPtr Data
		{
			get { return _cHeader.lpData; }
        }
        #endregion

        #region Methods
        public bool Record()
		{
			lock(this)
			{
				_cRecordEvent.Reset();
				_bRecording = waveInAddBuffer(_pWaveIn, ref _cHeader, Marshal.SizeOf(_cHeader)) == MMSYSERR.NOERROR;
				return _bRecording;
			}
		}

		public void WaitFor()
		{
			if (_bRecording)
				_bRecording = _cRecordEvent.WaitOne();
			else
				Thread.Sleep(0);
		}

		private void OnCompleted()
		{
			_cRecordEvent.Set();
			_bRecording = false;
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_cHeader.lpData != IntPtr.Zero)
            {
                waveInUnprepareHeader(_pWaveIn, ref _cHeader, Marshal.SizeOf(_cHeader));
                _hHeaderHandle.Free();
                _cHeader.lpData = IntPtr.Zero;
            }
            _cRecordEvent.Close();
            if (_hHeaderDataHandle.IsAllocated)
                _hHeaderDataHandle.Free();
            GC.SuppressFinalize(this);
        }
        #endregion
	}

	public class WaveInRecorder : IDisposable
    {
        #region Constants
        private const int CALLBACK_FUNCTION = 0x00030000;
        private const int WAVE_FORMAT_PCM = 1;
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct WAVEINCAPS
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szPname;
            public uint dwFormats;
            public short wChannels;
            public short wReserved;
        }
        #endregion

        #region API
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInOpen(out IntPtr phwi, int uDeviceID, ref WAVEFORMATEX lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInStart(IntPtr hwi);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInStop(IntPtr hwi);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInReset(IntPtr hwi);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInClose(IntPtr hwi);
        [DllImport("winmm.dll")]
        private static extern int waveInGetNumDevs();
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveInGetDevCaps(uint uDeviceID, out WAVEINCAPS pwoc, uint cbwoc);
        #endregion

        #region Fields/Delegates
        private bool _bFinished;
        private IntPtr _pWaveIn;
        private WAVEFORMATEX _tWaveFormat;
		private WaveInBuffer _cBuffers;
		private WaveInBuffer _cCurrentBuffer;
		private Thread _cThread;
		private BufferDoneEventHandler _eDoneProc;
        public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WAVEHDR wavhdr, int dwParam2);
		private WaveDelegate _cBufferProc = new WaveDelegate(WaveInBuffer.WaveInProc);
        #endregion

        #region Properties
        /// <summary>Play status</summary>
        public bool Recording { get; private set; }
        /// <summary>Channels in current track</summary>
        public ushort Channels { get; private set; }
        /// <summary>BPS in current track</summary>
        public ushort BitsPerSample { get; private set; }
        /// <summary>SPS of current track</summary>
        public uint SamplesPerSecond { get; private set; }
        /// <summary>Average BPS of current track</summary>
        public uint AvgBytesPerSecond { get; private set; }
        /// <summary>Recording format id</summary>
        public int Format { get; private set; }
        /// <summary>Internal buffer size</summary>
        public uint BufferSize { get; set; }
        /// <summary>Number of allocated buffers</summary>
        public uint BufferCount { get; set; }
        /// <summary>Recording device id</summary>
        public int Device { get; set; }
        #endregion

        #region Constructor
        public WaveInRecorder()
        {
        }
        /// <summary>Initialze class and set defaults</summary>
        /// <param name="device">device id</param>
        /// <param name="format">WAVEFORMATEX format description</param>
        /// <param name="doneProc">data callback</param>
        public WaveInRecorder(int device, ref WAVEFORMATEX format, BufferDoneEventHandler doneProc)
		{
			_eDoneProc = doneProc;
            format = WaveFormat(format.nSamplesPerSec, format.wBitsPerSample, format.nChannels);
            _tWaveFormat = format;
            this.Device = device;
            this.BufferSize = 4096;
            this.BufferCount = 4;
            this.Channels = _tWaveFormat.nChannels;
            this.BitsPerSample = _tWaveFormat.wBitsPerSample;
            this.SamplesPerSecond = _tWaveFormat.nSamplesPerSec;
            this.AvgBytesPerSecond = _tWaveFormat.nAvgBytesPerSec;
            this.Format = _tWaveFormat.wFormatTag;
		}
		~WaveInRecorder()
		{
			Dispose();
		}
        #endregion

        #region ThreadProc
		private void ThreadProc()
		{
			while (!_bFinished)
			{
				Advance();
				if (_eDoneProc != null && !_bFinished)
					_eDoneProc(_cCurrentBuffer.Data, _cCurrentBuffer.Size);
				_cCurrentBuffer.Record();
			}
		}
        #endregion

        #region Public Methods
        /// <summary>Begin recording</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Record()
        {
            MMSYSERR mmr = waveInOpen(out _pWaveIn, Device, ref _tWaveFormat, _cBufferProc, 0, CALLBACK_FUNCTION);
            if (mmr == MMSYSERR.NOERROR)
            {
                AllocateBuffers(BufferSize, BufferCount);
                for (uint i = 0; i < BufferCount; i++)
                {
                    SelectNextBuffer();
                    _cCurrentBuffer.Record();
                }
                waveInStart(_pWaveIn);
                _cThread = new Thread(new ThreadStart(ThreadProc));
                _cThread.Start();
                Recording = true;
            }
            return mmr;
        }

        /// <summary>Stop recording and reset</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Stop()
        {
            MMSYSERR mmr = MMSYSERR.ERROR;
            try
            {
                _bFinished = true;
                if (_pWaveIn != IntPtr.Zero)
                    waveInReset(_pWaveIn);
                if (_cThread != null)
                    _cThread.Abort();
                _eDoneProc = null;
                FreeBuffers();
                if (_pWaveIn != IntPtr.Zero)
                    mmr = waveInClose(_pWaveIn);
                else
                    mmr = MMSYSERR.NOERROR;
            }
            finally
            {
                _cThread = null;
                _pWaveIn = IntPtr.Zero;
                Recording = false;
            }
            return mmr;
        }

        /// <summary>Pause recording</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Pause()
        {
            _cThread.Suspend();
            Recording = false;
            return waveInStop(_pWaveIn);
        }

        /// <summary>Resujme recording</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Resume()
        {
            _cThread.Resume();
            Recording = true;
            return waveInStart(_pWaveIn);
        }

        /// <summary>Get the number of playback devices in the system</summary>
        /// <returns>number of devices</returns>
        public int GetDeviceCount()
		{
			return waveInGetNumDevs();
		}

        /// <summary>Get the input device name from device id</summary>
        /// <param name="deviceId">device id</param>
        /// <param name="prodName">returns device name</param>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR GetInputDeviceName(uint deviceId, ref string prodName)
        {
            WAVEINCAPS caps = new WAVEINCAPS();
            MMSYSERR result = waveInGetDevCaps(deviceId, out caps, (uint)Marshal.SizeOf(caps));
            if (result != MMSYSERR.NOERROR)
                return result;
            prodName = new string(caps.szPname);
            if (prodName.Contains("(") && !(prodName.Contains(")")))
            {
                prodName = prodName.Substring(0, prodName.IndexOf("("));
            }
            else if (prodName.Contains(")"))
            {
                if (prodName.IndexOf(")") > 8)
                {
                    prodName = prodName.Substring(0, prodName.IndexOf(")") + 1);
                }
            }
            return MMSYSERR.NOERROR;
        }

        /// <summary>Calculate a WAVEFORMATEX structure</summary>
        /// <param name="rate">bit rate</param>
        /// <param name="bits">bits per sample</param>
        /// <param name="channels">number of channels</param>
        /// <returns></returns>
        public WAVEFORMATEX WaveFormat(uint rate, ushort bits, ushort channels)
        {
            WAVEFORMATEX wfx = new WAVEFORMATEX();
            wfx.wFormatTag = WAVE_FORMAT_PCM;
            wfx.nChannels = channels;
            wfx.nSamplesPerSec = rate;
            wfx.wBitsPerSample = bits;
            wfx.nBlockAlign = (ushort)(channels * (bits / 8));
            wfx.nAvgBytesPerSec = wfx.nSamplesPerSec * wfx.nBlockAlign;
            wfx.cbSize = 0;
            return wfx;
        }

        /// <summary>Copies header to a stream</summary>
        /// <param name="waveData">Wav data stream</param>
        /// <param name="format">WAVEFORMATEX wav format</param>
        /// <returns>Stream</returns>
        public Stream CreateStream(Stream waveData, WAVEFORMATEX format)
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF".ToCharArray()));
            writer.Write((Int32)(waveData.Length + 36)); //File length minus first 8 bytes of RIFF description
            writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt ".ToCharArray()));
            writer.Write((Int32)16); //length of following chunk: 16
            writer.Write((Int16)format.wFormatTag);
            writer.Write((Int16)format.nChannels);
            writer.Write((Int32)format.nSamplesPerSec);
            writer.Write((Int32)format.nAvgBytesPerSec);
            writer.Write((Int16)format.nBlockAlign);
            writer.Write((Int16)format.wBitsPerSample);
            writer.Write(System.Text.Encoding.ASCII.GetBytes("data".ToCharArray()));
            writer.Write((Int32)waveData.Length);

            waveData.Seek(0, SeekOrigin.Begin);
            byte[] b = new byte[waveData.Length];
            waveData.Read(b, 0, (int)waveData.Length);
            writer.Write(b);
            writer.Seek(0, SeekOrigin.Begin);
            return stream;
        }
        #endregion

        #region Private Methods
        /// <summary>Allocate internal buffers</summary>
        /// <param name="bufferSize">size of buffer</param>
        /// <param name="bufferCount">number of buffers to allocate</param>
		private void AllocateBuffers(uint bufferSize, uint bufferCount)
		{
			FreeBuffers();
			if (bufferCount > 0)
			{
				_cBuffers = new WaveInBuffer(_pWaveIn, bufferSize);
				WaveInBuffer Prev = _cBuffers;
				try
				{
					for (int i = 1; i < bufferCount; i++)
					{
						WaveInBuffer Buf = new WaveInBuffer(_pWaveIn, bufferSize);
						Prev._cNextBuffer = Buf;
						Prev = Buf;
					}
				}
				finally
				{
					Prev._cNextBuffer = _cBuffers;
				}
			}
		}

        /// <summary>Free the internal buffers</summary>
		private void FreeBuffers()
		{
			_cCurrentBuffer = null;
			if (_cBuffers != null)
			{
				WaveInBuffer first = _cBuffers;
				_cBuffers = null;

				WaveInBuffer current = first;
				do
				{
					WaveInBuffer Next = current._cNextBuffer;
					current.Dispose();
					current = Next;
				} while(current != first);
			}
		}

        /// <summary>Advance to the next buffer</summary>
		private void Advance()
		{
			SelectNextBuffer();
			_cCurrentBuffer.WaitFor();
		}

        /// <summary>Select next internal buffer</summary>
		private void SelectNextBuffer()
		{
			_cCurrentBuffer = _cCurrentBuffer == null ? _cBuffers : _cCurrentBuffer._cNextBuffer;
		}

        /// <summary>Wait for buffers</summary>
		private void WaitForAllBuffers()
		{
			WaveInBuffer buf = _cBuffers;
			while (buf._cNextBuffer != _cBuffers)
			{
				buf.WaitFor();
				buf = buf._cNextBuffer;
			}
		}
        #endregion

        #region Dispose
        /// <summary>Dispose and release resources</summary>
        public void Dispose()
		{
            Stop();
			GC.SuppressFinalize(this);
		}
        #endregion
	}
}
