#region About
// ~Hats off to~
// Buffer routines code by: Ianier Munoz | ianier@hotmail.com
// http://www.codeproject.com/KB/audio-video/cswavrec.aspx?fid=16677&fr=276#xx0xx
// for original license refer to his code
// msdn article: Creating a PInvoke library in C# - http://msdn.microsoft.com/en-us/library/aa446550.aspx

// ToDo:
// format - done
// migrate wavenative (we don't need headers in c#) - done
// add pause/stop/play/pause/resume/device name/volume/position/length - done
// fix threading issues - done
// auto header - done
// acm (de)compression - done
// remove un-needed code - mmnn..
// comments - done
#endregion

#region Directives
using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
#endregion

namespace WaveLib
{
    #region Enums
    public enum MMSYSERR : int
    {
        NOERROR = 0,
        ERROR,
        BADDEVICEID,
        NOTENABLED,
        ALLOCATED,
        INVALHANDLE,
        NODRIVER,
        NOMEM,
        NOTSUPPORTED,
        BADERRNUM,
        INVALFLAG,
        INVALPARAM,
        HANDLEBUSY,
        INVALIDALIAS,
        BADDB,
        KEYNOTFOUND,
        READERROR,
        WRITEERROR,
        DELETEERROR,
        VALNOTFOUND,
        NODRIVERCB,
    }
    #endregion

    #region Structs
    [StructLayout(LayoutKind.Sequential)]
    public struct WAVEHDR
    {
        public IntPtr lpData; // pointer to locked data buffer
        public uint dwBufferLength; // length of data buffer
        public uint dwBytesRecorded; // used for input only
        public IntPtr dwUser; // for client's use
        public uint dwFlags; // assorted flags (see defines)
        public uint dwLoops; // loop control counter
        public IntPtr lpNext; // PWaveHdr, reserved for driver
        public uint reserved; // reserved for driver
    }
    #endregion

    #region Public Delegates
    public delegate void BufferFillEventHandler(IntPtr data, uint size);
    #endregion

    #region Class WaveOutBuffer
    internal class WaveOutBuffer : IDisposable
    {
        #region Enums
        private enum NMWOM : int
        {
            MM_WOM_OPEN = 0x3BB,
            MM_WOM_CLOSE = 0x3BC,
            MM_WOM_DONE = 0x3BD
        }
        #endregion

        #region API
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutPrepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutUnprepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutWrite(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, int uSize);
        #endregion

        #region Fields
        private bool _bPlaying;
        private IntPtr _pWaveOut;
        private GCHandle _hHeaderHandle;
        private GCHandle _hHeaderDataHandle;
        public WaveOutBuffer _cNextBuffer;
        private AutoResetEvent _cPlayEvent = new AutoResetEvent(false);
        private WAVEHDR _cHeader;
        private byte[] _btHeaderData;
        #endregion

        #region Proc
        internal static void WaveOutProc(IntPtr hdrvr, int uMsg, int dwUser, ref WAVEHDR wavhdr, int dwParam2)
        {
            if (uMsg == (int)NMWOM.MM_WOM_DONE)
            {
                try
                {
                    GCHandle h = (GCHandle)wavhdr.dwUser;
                    WaveOutBuffer buf = (WaveOutBuffer)h.Target;
                    buf.OnCompleted();
                }
                catch { }
            }
        }
        #endregion

        #region Constructor
        public WaveOutBuffer(IntPtr waveOutHandle, uint size)
        {
            _pWaveOut = waveOutHandle;
            _hHeaderHandle = GCHandle.Alloc(_cHeader, GCHandleType.Pinned);
            _cHeader.dwUser = (IntPtr)GCHandle.Alloc(this);
            _btHeaderData = new byte[size];
            _hHeaderDataHandle = GCHandle.Alloc(_btHeaderData, GCHandleType.Pinned);
            _cHeader.lpData = _hHeaderDataHandle.AddrOfPinnedObject();
            _cHeader.dwBufferLength = size;
            waveOutPrepareHeader(_pWaveOut, ref _cHeader, Marshal.SizeOf(_cHeader));
        }
        ~WaveOutBuffer()
        {
            Dispose();
        }
        #endregion

        #region Properties
        /// <summary>Get the buffer size</summary>
        public uint Size
        { get { return _cHeader.dwBufferLength; } }
        /// <summary>Get the data pointer</summary>
        public IntPtr Data
        { get { return _cHeader.lpData; } }
        #endregion

        #region Methods
        public void OnCompleted()
        {
            _cPlayEvent.Set();
            _bPlaying = false;
        }

        public bool Play()
        {
            lock (this)
            {
                _cPlayEvent.Reset();
                _bPlaying = waveOutWrite(_pWaveOut, ref _cHeader, Marshal.SizeOf(_cHeader)) == MMSYSERR.NOERROR;
                return _bPlaying;
            }
        }

        public void WaitFor()
        {
            if (_bPlaying)
                _bPlaying = _cPlayEvent.WaitOne();
            else
                Thread.Sleep(0);
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            if (_cHeader.lpData != IntPtr.Zero)
            {
                waveOutUnprepareHeader(_pWaveOut, ref _cHeader, Marshal.SizeOf(_cHeader));
                _hHeaderHandle.Free();
                _cHeader.lpData = IntPtr.Zero;
            }
            _cPlayEvent.Close();
            if (_hHeaderDataHandle.IsAllocated)
                _hHeaderDataHandle.Free();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    #endregion

    #region Class WaveOutPlayer
    public class WaveOutPlayer : IDisposable
    {
        #region Constants
        private const int WF_OFFSET_FORMATTAG = 20;
        private const int WF_OFFSET_CHANNELS = 22;
        private const int WF_OFFSET_SAMPLESPERSEC = 24;
        private const int WF_OFFSET_AVGBYTESPERSEC = 28;
        private const int WF_OFFSET_BLOCKALIGN = 32;
        private const int WF_OFFSET_BITSPERSAMPLE = 34;
        private const int WF_OFFSET_DATA = 44;
        private const int WAVE_FORMAT_PCM = 1;
        private const int CALLBACK_FUNCTION = 0x00030000;
        #endregion

        #region Enums
        private enum TIME_FORMAT : int
        {
            TIME_MS = 0x1,
            TIME_SAMPLES = 0x2,
            TIME_BYTES = 0x4,
            TIME_SMPTE = 0x8,
            TIME_MIDI = 0x10,
            TIME_MIDI_TICKS = 0x20
        }
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct MMTIME
        {
            public int wType;
            public int u;
            public int x;
            public MMTIME(int type, int units, int pos)
            {
                wType = type;
                u = units;
                x = pos;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct WAVEOUTCAPS
        {
            public short wMid;
            public short wPid;
            public int vDriverVersion;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szPname;
            public uint dwFormats;
            public short wChannels;
            public short wReserved;
            public uint dwSupport;
        }
        #endregion

        #region API
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutOpen(out IntPtr hWaveOut, Int32 uDeviceID, ref WAVEFORMATEX lpFormat, WaveDelegate dwCallback, int dwInstance, int dwFlags);
        [DllImport("winmm.dll")]
        private static extern uint waveOutGetNumDevs();
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutReset(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutClose(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutPause(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutRestart(IntPtr hWaveOut);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutGetPosition(IntPtr hWaveOut, ref MMTIME lpInfo, int uSize);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutSetVolume(IntPtr hWaveOut, uint dwVolume);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutGetVolume(IntPtr hWaveOut, out uint dwVolume);
        [DllImport("winmm.dll")]
        private static extern MMSYSERR waveOutGetDevCaps(uint uDeviceID, out WAVEOUTCAPS pwoc, uint cbwoc);
        #endregion

        #region Fields/Delegates
        private bool _bFinished = false;
        private byte _btZero;
        private IntPtr _pWaveOut = IntPtr.Zero;
        private WAVEFORMATEX _tWaveFormat = new WAVEFORMATEX();
        private WaveOutBuffer _cBuffers;
        private WaveOutBuffer _cCurrentBuffer;
        private Thread _cThread;
        private BufferFillEventHandler _eFillProc;
        public delegate void WaveDelegate(IntPtr hdrvr, int uMsg, int dwUser, ref WAVEHDR wavhdr, int dwParam2);
        private WaveDelegate _BufferProc = new WaveDelegate(WaveOutBuffer.WaveOutProc);
        #endregion

        #region Properties
        /// <summary>Play status</summary>
        public bool Playing { get; private set; }
        /// <summary>Channels in current track</summary>
        public ushort Channels { get; private set; }
        /// <summary>BPS in current track</summary>
        public ushort BitsPerSample { get; private set; }
        /// <summary>Data length</summary>
        public uint Length { get; private set; }
        /// <summary>SPS of current track</summary>
        public uint SamplesPerSecond { get; private set; }
        /// <summary>Average BPS of current track</summary>
        public uint AvgBytesPerSecond { get; private set; }
        /// <summary>Internal buffer size</summary>
        public uint BufferSize { get; set; }
        /// <summary>Number of allocated buffers</summary>
        public uint BufferCount { get; set; }
        /// <summary>Playback device id</summary>
        public int Device { get; set; }
        /// <summary>Convert to 16 bit</summary>
        public bool Convert16Bit { get; set; }
        /// <summary>Convert to 2 channels</summary>
        public bool Convert2Channels { get; set; }
        #endregion

        #region Constructor
        public WaveOutPlayer(int deviceId, BufferFillEventHandler fillProc)
        {
            _eFillProc = fillProc;
            // load defaults
            this.Playing = false;
            this.Device = deviceId;
            this.Length = 0;
            this.BufferSize = 1024;//defaults 4096 * 4
            this.BufferCount = 16;
        }
        public WaveOutPlayer()
        {
        }
        ~WaveOutPlayer()
        {
            Dispose();
        }
        #endregion

        #region Thread Delegate
        /// <summary>Play buffer delegate</summary>
        private void ThreadProc()
        {
            while (!_bFinished)
            {
                Advance();
                if (_eFillProc != null && !_bFinished)
                {
                    _eFillProc(_cCurrentBuffer.Data, _cCurrentBuffer.Size);
                }
                else
                {
                    // zero out buffer
                    byte v = _btZero;
                    byte[] b = new byte[_cCurrentBuffer.Size];
                    for (int i = 0; i < b.Length; i++)
                        b[i] = v;
                    Marshal.Copy(b, 0, _cCurrentBuffer.Data, b.Length);

                }
                _cCurrentBuffer.Play();
            }
            WaitForAllBuffers();
        }
        #endregion

        #region Public Methods
        /// <summary>Get the number of playback devices in the system</summary>
        /// <returns>number of devices</returns>
        public uint GetDeviceCount()
        {
            return waveOutGetNumDevs();
        }

        /// <summary>Get the output device name from device id</summary>
        /// <param name="deviceId">device id</param>
        /// <param name="prodName">returns device name</param>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR GetOutputDeviceName(uint deviceId, ref string prodName)
        {
            WAVEOUTCAPS caps = new WAVEOUTCAPS();
            MMSYSERR result = waveOutGetDevCaps(deviceId, out caps, (uint)Marshal.SizeOf(caps));
            if (result != MMSYSERR.NOERROR)
                return result;
            //WAVEOUTCAPS2 often does not return product name GUID, as it is not
            //a required field, so is most often left blank..
            //The only way to get full string in vista/w7 that I have come across is
            //by enumerating devices with directsound, but that would mean referencing
            //the library, so.. deal with it
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

        /// <summary>Copy wav data to a memorystream with auto conversion</summary>
        /// <param name="file">file name</param>
        /// <param name="stream">destination stream</param>
        /// <returns>bool -success</returns>
        public bool CopyStream(string file, ref MemoryStream stream)
        {
            try
            {
                // try reading the header
                GetHeader(file, ref _tWaveFormat);
                // Houston, we have a problem.. this requires conversion
                if (_tWaveFormat.wBitsPerSample == 0 || _tWaveFormat.wFormatTag != WAVE_FORMAT_PCM ||
                    (this.Convert16Bit == true && _tWaveFormat.wBitsPerSample == 8) ||
                    (this.Convert2Channels == true && _tWaveFormat.nChannels == 1))
                {
                    AcmStreamOut acm = new AcmStreamOut();
                    // conversion options
                    acm.Convert16Bit = this.Convert16Bit;
                    acm.Convert2Channels = this.Convert2Channels;
                    // fetch the converted stream
                    SND_RESULT ret = acm.PreConvert(file, ref stream);

                    if (ret != SND_RESULT.SND_ERR_END_OF_STREAM || stream.Length == 0)
                    {
                        return false;
                    }
                    else
                    {
                        // get the header and set props
                        _tWaveFormat = acm.GetHeader();
                        this.Length = acm.DataLength;
                        this.Channels = _tWaveFormat.nChannels;
                        this.SamplesPerSecond = _tWaveFormat.nSamplesPerSec;
                        this.BitsPerSample = _tWaveFormat.wBitsPerSample;
                        this.AvgBytesPerSecond = _tWaveFormat.nAvgBytesPerSec;
                        acm.Close();
                        stream.Position = 0;
                    }
                }
                else
                {
                    // does not require conversion
                    int offset = WF_OFFSET_DATA;
                    // open a filestream and reader
                    FileStream fs = new FileStream(file, FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    // copy to byte array
                    int len = (int)this.Length;
                    byte[] bt = new byte[len];
                    fs.Position = offset;
                    br.Read(bt, 0, bt.Length);
                    fs.Close();
                    // write to stream
                    stream = new MemoryStream(bt.Length);
                    stream.Write(bt, 0, bt.Length);
                    stream.Position = 0;
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>Get the playback position in the stream</summary>
        /// <returns>position</returns>
        public uint GetPosition()
        {
            if (!this.Playing) return 0;
            // set up the struct
            MMTIME udtTime = new MMTIME();
            udtTime.wType = (int)TIME_FORMAT.TIME_BYTES;
            // pass it in
            waveOutGetPosition(_pWaveOut, ref udtTime, Marshal.SizeOf(udtTime));
            // format time
            int x = (int)(udtTime.u / this.AvgBytesPerSecond) * 1000;
            switch (udtTime.wType)
            {
                case (int)TIME_FORMAT.TIME_BYTES:
                    return (uint)(udtTime.u / this.AvgBytesPerSecond) * 1000;
                case (int)TIME_FORMAT.TIME_SAMPLES:
                    return (uint)(udtTime.u / this.SamplesPerSecond) * 1000;
                case (int)TIME_FORMAT.TIME_MS:
                    return (uint)udtTime.u;
                default:
                    return 0;
            }
        }

        /// <summary>Get the current volume levels</summary>
        /// <param name="left">returns left channel</param>
        /// <param name="right">returns right channel</param>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR GetVolume(ref uint left, ref uint right)
        {
            uint vol = 0;

            MMSYSERR err = waveOutGetVolume(_pWaveOut, out vol);
            if (err != MMSYSERR.NOERROR)
                return err;

            left = (vol & 0x00ff);
            right = (vol >> 8);

            return err;
        }

        /// <summary>Set the volume level</summary>
        /// <param name="left">left channel</param>
        /// <param name="right">right channel</param>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR SetVolume(uint left, uint right)
        {
            uint vol = (left & 0x00ff) | (right << 8);
            MMSYSERR err = MMSYSERR.NOERROR;
            err = waveOutSetVolume(_pWaveOut, vol);
            // probably poor ol' vista
            if (err != MMSYSERR.NOERROR)
            {
                Mixer mx = new Mixer();
                mx.OpenMixer();
                err = mx.SetVolume(vol);
                mx.CloseMixer();
            }
            return err;
        }

        /// <summary>Start playback - must call CopyStream() first</summary>
        /// <param name="file">file name</param>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Play(string file)
        {
            if (this.Playing) 
                Stop();
            _btZero = _tWaveFormat.wBitsPerSample == 8 ? (byte)128 : (byte)0;

            MMSYSERR err = waveOutOpen(out _pWaveOut, this.Device, ref _tWaveFormat, _BufferProc, 0, CALLBACK_FUNCTION);

            // if success
            if (err == MMSYSERR.NOERROR)
            {
                this.Playing = true;
                // create buffers and launch thread
                AllocateBuffers(this.BufferSize, this.BufferCount);
                _cThread = new Thread(new ThreadStart(ThreadProc));
                _cThread.Start();
            }
            return err;
        }

        /// <summary>Pause playback</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Pause()
        {
            return waveOutPause(_pWaveOut);
        }

        /// <summary>Resume playback</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Resume()
        {
            return waveOutRestart(_pWaveOut);
        }

        /// <summary>Stop playback, and cleanup resources</summary>
        /// <returns>MMSYSERR</returns>
        public MMSYSERR Stop()
        {
            MMSYSERR mmr = MMSYSERR.ERROR;
            try
            {
                _bFinished = true;
                if (_pWaveOut != IntPtr.Zero)
                    waveOutReset(_pWaveOut);
                if (_cThread != null)
                    _cThread.Abort();
                _eFillProc = null;
                FreeBuffers();
                if (_pWaveOut != IntPtr.Zero)
                    mmr = waveOutClose(_pWaveOut);
                else
                    mmr = MMSYSERR.NOERROR;
            }
            finally
            {
                _cThread = null;
                _pWaveOut = IntPtr.Zero;
                this.Playing = false;
            }
            return mmr;
        }
        #endregion

        #region Private Methods
        /// <summary>Advance buffer</summary>
        private void Advance()
        {
            _cCurrentBuffer = _cCurrentBuffer == null ? _cBuffers : _cCurrentBuffer._cNextBuffer;
            _cCurrentBuffer.WaitFor();
        }

        /// <summary>Allocate buffers</summary>
        /// <param name="bufferSize">buffer size</param>
        /// <param name="bufferCount">buffer count</param>
        private void AllocateBuffers(uint bufferSize, uint bufferCount)
        {
            FreeBuffers();
            if (bufferCount > 0)
            {
                _cBuffers = new WaveOutBuffer(_pWaveOut, bufferSize);
                WaveOutBuffer Prev = _cBuffers;
                try
                {
                    for (int i = 1; i < bufferCount; i++)
                    {
                        WaveOutBuffer wbf = new WaveOutBuffer(_pWaveOut, bufferSize);
                        Prev._cNextBuffer = wbf;
                        Prev = wbf;
                    }
                }
                finally
                {
                    Prev._cNextBuffer = _cBuffers;
                }
            }
        }

        /// <summary>Release buffers</summary>
        private void FreeBuffers()
        {
            _cCurrentBuffer = null;
            if (_cBuffers != null)
            {
                WaveOutBuffer First = _cBuffers;
                _cBuffers = null;

                WaveOutBuffer Current = First;
                do
                {
                    WaveOutBuffer Next = Current._cNextBuffer;
                    Current.Dispose();
                    Current = Next;
                } while (Current != First);
            }
        }

        /// <summary>Seek to position in stream</summary>
        /// <param name="fs">Stream</param>
        public void SeekTo(Stream fs)
        {
            fs.Seek(WF_OFFSET_FORMATTAG, SeekOrigin.Begin);
        }

        /// <summary>Read header from stream</summary>
        /// <param name="rdr">binary reader</param>
        /// <param name="fmt">WAVEFORMATEX structure</param>
        private void ReadHeader(BinaryReader rdr, ref WAVEFORMATEX fmt)
        {
            fmt.wFormatTag = rdr.ReadUInt16();
            fmt.nChannels = rdr.ReadUInt16();
            fmt.nSamplesPerSec = rdr.ReadUInt32();
            fmt.nAvgBytesPerSec = rdr.ReadUInt32();
            fmt.nBlockAlign = rdr.ReadUInt16();
            fmt.wBitsPerSample = rdr.ReadUInt16();
            // Unused subchunk Id and size
            uint dataId = rdr.ReadUInt32();
            uint dataLength = rdr.ReadUInt32();
        }

        /// <summary>Read wav header using binary read method</summary>
        /// <param name="file">file name</param>
        /// <param name="wf">WAVEFORMATEX structure</param>
        private void GetHeader(string file, ref WAVEFORMATEX wf)
        {
            FileStream strm = new FileStream(file, FileMode.Open);
            BinaryReader rdr = new BinaryReader(strm);
            // get the header data
            SeekTo(strm);
            ReadHeader(rdr, ref wf);
            // set class properties
            this.Length = (uint)(rdr.BaseStream.Length - WF_OFFSET_DATA);
            this.Channels = wf.nChannels;
            this.SamplesPerSecond = wf.nSamplesPerSec;
            this.BitsPerSample = wf.wBitsPerSample;
            this.AvgBytesPerSecond = wf.nAvgBytesPerSec;
            // cleanup
            rdr.Close();
            strm.Close();
        }

        /// <summary>Wait for buffers to release</summary>
        private void WaitForAllBuffers()
        {
            WaveOutBuffer bf = _cBuffers;
            while (bf._cNextBuffer != _cBuffers)
            {
                bf.WaitFor();
                bf = bf._cNextBuffer;
            }
        }
        #endregion

        #region Dispose
        /// <summary>Cleanup</summary>
        public void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
    #endregion
}
