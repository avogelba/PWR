#region About
// helpful links
// http://msdn.microsoft.com/en-us/library/aa505945.aspx
// http://msdn.microsoft.com/en-us/library/dd743586(VS.85).aspx
// ~Hats Off to~
// Based on Arne Elster' WaveOut player (vb6) - http://www.planet-source-code.com/vb/scripts/ShowCode.asp?txtCodeId=66866&lngWId=1
#endregion

#region Directives
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
#endregion

namespace WaveLib
{
    #region Public Enum
    public enum SND_RESULT
    {
        SND_ERR_SUCCESS,
        SND_ERR_INVALID_SOURCE,
        SND_ERR_INVALID_OUTPUT,
        SND_ERR_INTERNAL,
        SND_ERR_OUT_OF_RANGE,
        SND_ERR_END_OF_STREAM,
        SND_ERR_INVALID_TAG,
        SND_ERR_INVALID_PARAM,
        SND_ERR_TOO_BIG,
        SND_ERR_NEED_MORE,
        SND_ERR_UNKNOWN
    }
    #endregion

    #region Public Structs
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
    public struct WAVEFORMATEX
    {
        public ushort wFormatTag;
        public ushort nChannels;
        public uint nSamplesPerSec;
        public uint nAvgBytesPerSec;
        public ushort nBlockAlign;
        public ushort wBitsPerSample;
        public ushort cbSize;
    }
    #endregion

    unsafe class AcmStreamOut
    {
        #region Constants
        private const string Extensions = "WAV";
        private const string Description = "Wave Audio";
        private const int MMIO_READ = 0x0;
        private const int MMIO_FINDCHUNK = 0x10;
        private const int MMIO_FINDRIFF = 0x20;
        private const int SEEK_CUR = 1;
        private const int WAVE_FORMAT_PCM = 1;

        private const int WAVE_FORMAT_DOLBY_AC3_SPDIF = 0x0092;
        private const int WAVE_FORMAT_EXTENSIBLE = 0xFFFE;
        private const int WAVE_FORMAT_IEEE_FLOAT = 0x0003;
        private const int WAVE_FORMAT_DRM = 0x0009;
        private const int WAVE_FORMAT_ALAW = 0x0006;
        private const int WAVE_FORMAT_MULAW = 0x0007;
        private const int WAVE_FORMAT_ADPCM = 0x0002;
        private const int WAVE_FORMAT_MPEG = 0x0050;
        private const int WAVE_FORMAT_WMASPDIF = 0x0164;

        private const int HEAP_ZERO_MEMORY = 0x00000008;
        private const int ACM_STREAMCONVERTF_BLOCKALIGN = 0x00000004;
        private const int ACM_STREAMCONVERTF_START = 0x00000010;
        private const int ACM_STREAMCONVERTF_END = 0x00000020;
        private const uint ACMSTREAMHEADER_STATUSF_DONE = 0x00010000;
        private const uint ACMSTREAMHEADER_STATUSF_PREPARED = 0x00020000;
        private const uint ACMSTREAMHEADER_STATUSF_INQUEUE = 0x00100000;
        private const int ACM_STREAMOPENF_QUERY = 0x00000001;
        private const int ACM_STREAMOPENF_ASYNC = 0x00000002;
        private const int ACM_STREAMOPENF_NONREALTIME = 0x00000004;
        private const int CALLBACK_EVENT = 0x50000;
        private const int OUTPUT_BUFFER_MS = 500;
        private static IntPtr INVALID_HANDLE = new IntPtr(-1);
        private static IntPtr INVALID_STREAM_HANDLE = new IntPtr(0);
        #endregion

        #region Enums
        private enum MMSYSERR : int
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

        private enum HACMSTREAM : int
        {
            INVALID_STREAM_HANDLE = 0
        }

        private enum ACM_STREAMSIZEF : int
        {
            ACM_STREAMSIZEF_DESTINATION = 0x1,
            ACM_STREAMSIZEF_SOURCE = 0x0,
            ACM_STREAMSIZEF_QUERYMASK = 0xF
        }

        private enum ACM_STREAMCONVERTF : int
        {
            ACM_STREAMCONVERTF_BLOCKALIGN = 0x4,
            ACM_STREAMCONVERTF_START = 0x10,
            ACM_STREAMCONVERTF_END = 0x20
        }

        private enum SEEK_METHOD : int
        {
            FILE_BEGIN = 0,
            FILE_CURRENT = 1,
            FILE_END = 2
        }

        private enum FILE_METHOD : int
        {
            CREATE_NEW = 1,
            CREATE_ALWAYS = 2,
            OPEN_EXISTING = 3,
            OPEN_ALWAYS = 4
        }

        private enum FILE_SHARE : int
        {
            FILE_SHARE_READ = 0x1,
            FILE_SHARE_WRITE = 0x2
        }

        private enum FILE_ACCESS : uint
        {
            GENERIC_READ = 0x80000000,
            GENERIC_WRITE = 0x40000000
        }

        public enum SND_SEEK_MODE : int
        {
            SND_SEEK_PERCENT,
            SND_SEEK_SECONDS
        }
        #endregion

        #region Structs
        private struct WAVEFILTER
        {
            public uint cbStruct;
            public uint dwFilterTag;
            public uint fdwFilter;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public uint[] dwReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MMIOINFO
        {
            public uint dwFlags;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string fccIOProc;
            public IntPtr pIOProc;
            public uint wErrorRet;
            public IntPtr htask;
            public long cchBuffer;
            public IntPtr pchBuffer;
            public IntPtr pchNext;
            public IntPtr pchEndRead;
            public IntPtr pchEndWrite;
            public long lBufOffset;
            public long lDiskOffset;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] adwInfo;
            public uint dwReserved1;
            public uint dwReserved2;
            public IntPtr hmmio;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ACMSTREAMHEADER
        {
            public uint cbStruct;
            public uint fdwStatus;
            public UIntPtr dwUser;
            public byte* pbSrc;
            public uint cbSrcLength;
            public uint cbSrcLengthUsed;
            public UIntPtr dwSrcUser;
            public byte* pbDst;
            public uint cbDstLength;
            public uint cbDstLengthUsed;
            public UIntPtr dwDstUser;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public uint[] dwReservedDriver;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHUNKINFO
        {
            public uint Start;
            public uint Length;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MMCKINFO
        {
            public uint ckid;
            public uint ckSize;
            public uint fccType;
            public uint dwDataOffset;
            public uint dwFlags;
        }
        #endregion

        #region API
        #region ACM
        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamOpen([In]IntPtr* handle, IntPtr driverHandle, byte* source, WAVEFORMATEX* dest, IntPtr WAVEFILTER, UIntPtr Callback, UIntPtr CallbackInstanceData, uint fdwOpen);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamSize(IntPtr has, uint cbInput, uint* pdwOutputBytes, uint fdwSize);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamClose(IntPtr has, uint fdwClose);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamPrepareHeader(IntPtr has, ref ACMSTREAMHEADER pash, uint fdwPrepare);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamConvert(IntPtr has, ref ACMSTREAMHEADER pash, uint fdwConvert);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmStreamUnprepareHeader(IntPtr has, ref ACMSTREAMHEADER pash, uint fdwUnprepare);

        [DllImport("msacm32.dll")]
        private static extern MMSYSERR acmFormatSuggest(IntPtr had, WAVEFORMATEX* source, ref WAVEFORMATEX dest, uint cbwfxDst, uint fdwSuggest);
        #endregion

        #region MMIO
        [DllImport("winmm.dll")]
        private static extern int mmioClose(IntPtr hmmio, uint uFlags);

        [DllImport("winmm.dll")]
        private static extern int mmioDescend(IntPtr hmmio, MMCKINFO* mm, MMCKINFO* lpckParent, uint uFlags);

        [DllImport("winmm.dll", EntryPoint = "mmioDescend")]
        private static extern int mmioDescendParent(IntPtr hmmio, MMCKINFO* lpck, IntPtr x, uint uFlags);

        [DllImport("winmm.dll")]
        private static extern IntPtr mmioOpen(string szFileName, IntPtr lpmmioinfo, uint dwOpenFlags);

        [DllImport("winmm.dll")]
        private static extern int mmioSeek(IntPtr hmmio, int lOffset, int iOrigin);

        [DllImport("winmm.dll")]
        private static extern uint mmioStringToFOURCC(string sz, uint uFlags);
        #endregion

        #region Memory
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr HeapAlloc(IntPtr hHeap, uint dwFlags, uint dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcessHeap();

        [DllImport("Kernel32.dll", SetLastError = false)]
        private static extern void RtlZeroMemory(byte* dest, uint size);

        [DllImport("Kernel32.dll", SetLastError = false)]
        private static extern void RtlMoveMemory(byte* dest, byte* src, uint size);

        [DllImport("Kernel32.dll", SetLastError = false)]
        private static extern void RtlMoveMemory(WAVEFORMATEX* dest, byte* src, uint size);

        [DllImport("Kernel32.dll", SetLastError = false)]
        private static extern void RtlMoveMemory(byte* dest, Int16* src, uint size);
        #endregion

        #region File
        [DllImport("kernel32.dll")]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr SecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ReadFile(IntPtr hFile, [Out]byte* lpBuffer, uint nNumberOfBytesToRead, [In, Out]uint* lpNumberOfBytesRead, [Out]IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WriteFile(IntPtr hFile, IntPtr lpBuffer, uint nNumberOfBytesToWrite, [Out]uint* lpNumberOfBytesWritten, [In, Out]IntPtr lpOverlapped);

        [DllImport("Kernel32.dll")]
        private static extern uint SetFilePointer(IntPtr hFile, int lDistanceToMove, [Out]IntPtr lpDistanceToMoveHigh, uint dwMoveMethod);

        [DllImport("kernel32.dll")]
        private static extern uint GetFileSize(IntPtr hFile, UIntPtr lpFileSizeHigh);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
        #endregion
        #endregion

        #region Fields
        private bool _bInEndOfStream = false;
        private bool _bInFirst = false;
        private uint _iBufferData = 0;
        private uint _iPosInBuffer = 0;
        private uint _iInputLen = 0;
        private uint _iOutputLen = 0;
        private uint _iKeepInBuffer = 0;
        private uint _iFilePositionMS = 0;
        private IntPtr _waveHandle;
        private IntPtr _hStream = INVALID_STREAM_HANDLE;
        private byte[] _btWfx;
        private byte[] _btInput;
        private byte[] _btOutput;
        private CHUNKINFO _ckData;
        private CHUNKINFO _ckInfo;
        private WAVEFORMATEX _tWFXIn = new WAVEFORMATEX();
        private WAVEFORMATEX _tWFXOut = new WAVEFORMATEX();
        #endregion

        #region Public properties
        /// <summary>Convert to 16 bit</summary>
        public bool Convert16Bit { get; set; }
        /// <summary>Convert to 2 channels</summary>
        public bool Convert2Channels { get; set; }
        /// <summary>Get data length</summary>
        public uint DataLength { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>Converts the entire file</summary>
        /// <param name="file">file name</param>
        /// <param name="stream">destination stream</param>
        /// <returns>SND_RESULT</returns>
        public SND_RESULT PreConvert(string file, ref MemoryStream stream)
        {
            SND_RESULT ret;
            byte[] bt = new byte[1024];
            stream = new MemoryStream();
            // start convertor
            Create(file);
            // loop through file and add to stream
            do
            {
                ret = Read(ref bt, 1024);
                if (ret == SND_RESULT.SND_ERR_SUCCESS)
                    stream.Write(bt, 0, bt.Length);
            } while (ret == SND_RESULT.SND_ERR_SUCCESS);
            // cleanup
            Close();
            stream.Position = 0;

            return ret;
        }

        /// <summary>Create the stream and initialize settings</summary>
        /// <param name="file">file name</param>
        /// <returns>SND_RESULT</returns>
        public SND_RESULT Create(string file)
        {
            if (!IsValidFile(file))
                return SND_RESULT.SND_ERR_INVALID_SOURCE;
            // find wav chunks data and fmt
            _ckData = GetChunkPos(file, "data");
            _ckInfo = GetChunkPos(file, "fmt ");
            DataLength = _ckData.Length;

            // valid chunks?
            if (_ckData.Start == 0)
                return SND_RESULT.SND_ERR_INVALID_SOURCE;
            if (_ckInfo.Start == 0)
                return SND_RESULT.SND_ERR_INVALID_SOURCE;
            if (_ckInfo.Length < 16)
                return SND_RESULT.SND_ERR_INVALID_SOURCE;

            // open file
            _waveHandle = FileOpen(file, FILE_ACCESS.GENERIC_READ, FILE_SHARE.FILE_SHARE_READ, FILE_METHOD.OPEN_EXISTING);
            if (_waveHandle == INVALID_HANDLE)
                return SND_RESULT.SND_ERR_INVALID_SOURCE;

            // shrink data chunks with ilegal length to file length
            if (FileLength(_waveHandle) < (_ckData.Start + _ckData.Length))
                _ckData.Length = FileLength(_waveHandle) - _ckData.Start;
            // read info chunk
            _btWfx = new byte[_ckInfo.Length];
            FileSeek(_waveHandle, (int)_ckInfo.Start, (uint)SEEK_METHOD.FILE_BEGIN);
            fixed (byte* pBt = _btWfx)
            { FileRead(_waveHandle, pBt, _ckInfo.Length); }

            // copy the header
            uint size = (uint)sizeof(WAVEFORMATEX);
            fixed (byte* pBt = _btWfx) fixed (WAVEFORMATEX* pWv = &_tWFXIn)
            { { RtlMoveMemory(pWv, pBt, size); } }

            // seek to the beginning of the audio data
            FileSeek(_waveHandle, (int)_ckData.Start, (uint)SEEK_METHOD.FILE_BEGIN);

            // init the Audio Compression Manager
            if (InitConversion() != MMSYSERR.NOERROR)
            {
                Close();
                return SND_RESULT.SND_ERR_INTERNAL;
            }
            return SND_RESULT.SND_ERR_SUCCESS;
        }

        /// <summary>Read converted byte stream</summary>
        /// <summary>If reading real time, first call must be proceeded by Create()</summary>
        /// <param name="data">input buffer</param>
        /// <param name="size">buffer size</param>
        /// <returns>SND_RESULT</returns>
        public SND_RESULT Read(ref byte[] data, uint size)
        {
            uint nread = 0;
            SND_RESULT ret = StreamRead(ref data, size, ref nread);
            if (nread == 0)
                return SND_RESULT.SND_ERR_END_OF_STREAM;
            return ret;
        }

        /// <summary>Close file and convertor</summary>
        public void Close()
        {
            CloseConverter();
            FileClose(_waveHandle);
            _waveHandle = IntPtr.Zero;
        }
        #endregion

        #region Private Methods
        #region Memory
        /// <summary>Allocate heap memory</summary>
        /// <param name="size">size desired</param>
        /// <returns>memory address</returns>
        private IntPtr Alloc(uint size)
        {
            return HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, size);
        }

        /// <summary>Release heap memory</summary>
        /// <param name="pmem">memory address</param>
        private void Free(IntPtr pmem)
        {
            HeapFree(GetProcessHeap(), 0, pmem);
        }
        #endregion

        #region File
        /// <summary>Tests file validity</summary>
        /// <param name="file">file</param>
        /// <returns>bool</returns>
        private bool IsValidFile(string file)
        {
            IntPtr hfile = FileOpen(file, FILE_ACCESS.GENERIC_READ, FILE_SHARE.FILE_SHARE_READ, FILE_METHOD.OPEN_EXISTING);
            if (hfile != INVALID_HANDLE)
            {
                FileClose(hfile);
                return true;
            }
            return false;
        }

        /// <summary>Open the wav file</summary>
        /// <param name="file">file name</param>
        /// <param name="access">access level</param>
        /// <param name="share">share method</param>
        /// <param name="method">open method</param>
        /// <returns>handle of file</returns>
        private IntPtr FileOpen(string file, FILE_ACCESS access, FILE_SHARE share, FILE_METHOD method)
        {
            return CreateFile(file, (uint)access, (uint)share, IntPtr.Zero, (uint)method, 0, IntPtr.Zero);
        }

        /// <summary>Close the wav file</summary>
        /// <param name="hfile">file handle</param>
        private void FileClose(IntPtr hfile)
        {
            CloseHandle(hfile);
        }

        /// <summary>Read from wav file</summary>
        /// <param name="hfile">file handle</param>
        /// <param name="pbuff">buffer pointer</param>
        /// <param name="nsize">size to read</param>
        /// <returns>bytes read</returns>
        private uint FileRead(IntPtr hfile, byte* pbuff, uint nsize)
        {
            uint nread = 0;
            uint* pread = &nread;

            if (hfile != INVALID_HANDLE)
                ReadFile(hfile, pbuff, nsize, pread, IntPtr.Zero);
            return nread;
        }

        /// <summary>Write to wav file</summary>
        /// <param name="hfile">file handle</param>
        /// <param name="pbuff">buffer pointer</param>
        /// <param name="nsize">size of buffer</param>
        /// <returns>bytes written</returns>
        private uint FileWrite(IntPtr hfile, IntPtr pbuff, uint nsize)
        {
            uint nwritten = 0;
            uint* pwritten = &nwritten;

            if (hfile != INVALID_HANDLE)
                WriteFile(hfile, pbuff, nsize, pwritten, IntPtr.Zero);
            return nwritten;
        }

        /// <summary></summary>
        /// <param name="hfile">file handle</param>
        /// <param name="npos">move file pointer to this position</param>
        /// <param name="method">file seek method: SEEK_METHOD</param>
        /// <returns>file pointer position</returns>
        private uint FileSeek(IntPtr hfile, int npos, uint method)
        {
            return SetFilePointer(hfile, npos, IntPtr.Zero, method);
        }

        /// <summary>Get position of read pointer in file</summary>
        /// <param name="hfile">file handle</param>
        /// <returns>file pointer position</returns>
        private uint FilePosition(IntPtr hfile)
        {
            return FileSeek(hfile, 0, (uint)SEEK_METHOD.FILE_CURRENT);
        }

        /// <summary>Get the wav file size</summary>
        /// <param name="hfile">file handle</param>
        /// <returns>file size</returns>
        private uint FileLength(IntPtr hfile)
        {
            return GetFileSize(hfile, UIntPtr.Zero);
        }

        /// <summary>At end of wav file</summary>
        /// <param name="hfile">file handle</param>
        /// <returns>bool</returns>
        private bool FileEnd(IntPtr hfile)
        {
            return FilePosition(hfile) >= FileLength(hfile);
        }
        #endregion

        #region Conversion
        /// <summary>Create a stream and size buffers</summary>
        /// <returns>bool</returns>
        private MMSYSERR InitConversion()
        {
            MMSYSERR mmr;

            if (_hStream != INVALID_STREAM_HANDLE)
                CloseConverter();

            _tWFXOut = _tWFXIn;

            if (_tWFXOut.wBitsPerSample < 8)
                _tWFXOut.wBitsPerSample = 8;
            else if (_tWFXOut.wBitsPerSample > 8)
                _tWFXOut.wBitsPerSample = 16;
            // force conversion to 16bit
            if (Convert16Bit)
                _tWFXOut.wBitsPerSample = 16;
            if (Convert2Channels)
                _tWFXOut.nChannels = 2;

            // create the new format
            _tWFXOut = CreateWFX(_tWFXOut.nSamplesPerSec, _tWFXOut.nChannels, _tWFXOut.wBitsPerSample);

          //  if (_tWFXOut.wFormatTag == WAVE_FORMAT_ADPCM || _tWFXOut.wFormatTag == WAVE_FORMAT_PCM)
          //      _tWFXOut.cbSize = 0;

            // open stream
            fixed (IntPtr* pSt = &_hStream) fixed (byte* pBt = _btWfx) fixed (WAVEFORMATEX* pWOut = &_tWFXOut)
            { { { mmr = acmStreamOpen(pSt, IntPtr.Zero, pBt, pWOut, IntPtr.Zero, UIntPtr.Zero, UIntPtr.Zero, ACM_STREAMOPENF_NONREALTIME); } } }

            // failed, try going to defaults
            if (mmr != MMSYSERR.NOERROR)
            {
                // try changing bps
                if (_tWFXOut.wBitsPerSample == 16)
                    _tWFXOut.wBitsPerSample = 8;
                else
                    _tWFXOut.wBitsPerSample = 16;

                if (Convert2Channels)
                {
                    if (_tWFXIn.nChannels == 1)
                        _tWFXOut.nChannels = 1;
                }

                // try again
                fixed (WAVEFORMATEX* pWOut = &_tWFXOut, pWIn = &_tWFXIn) fixed (IntPtr* pSt = &_hStream) fixed (byte* pBt = _btWfx)
                { { { mmr = acmStreamOpen(pSt, IntPtr.Zero, pBt, pWOut, IntPtr.Zero, UIntPtr.Zero, UIntPtr.Zero, 0); } } }

                // failed
                if (mmr != MMSYSERR.NOERROR)
                    return mmr;
            }
            // divide 500 by 1000 should get -2 on int.. nope
            // set size of output buffer
            //Decimal sx = (int)Decimal.Divide(1000 / OUTPUT_BUFFER_MS);
            _iOutputLen = (uint)(_tWFXOut.nAvgBytesPerSec / 2);

            // needed size of input buffer to fill the output buffer
            fixed (uint* pInLen = &_iInputLen)
            { mmr = acmStreamSize(_hStream, _iOutputLen, pInLen, (uint)ACM_STREAMSIZEF.ACM_STREAMSIZEF_DESTINATION); }

            // failed
            if (mmr != MMSYSERR.NOERROR)
            {
                acmStreamClose(_hStream, 0);
                _hStream = INVALID_STREAM_HANDLE;
                return mmr;
            }

            // success
            _btOutput = new byte[_iOutputLen];
            _btInput = new byte[_iInputLen];
            _bInEndOfStream = false;
            _bInFirst = true;
            _iKeepInBuffer = 0;
            return MMSYSERR.NOERROR;
        }

        /// <summary>Close the convertor and release resources</summary>
        private void CloseConverter()
        {
            try
            {
                if (_hStream != INVALID_STREAM_HANDLE)
                    acmStreamClose(_hStream, 0);
                _hStream = INVALID_STREAM_HANDLE;
                RealeaseBuffers();
                _bInEndOfStream = false;
                _iBufferData = 0;
                _iPosInBuffer = 0;
                _iInputLen = 0;
                _iOutputLen = 0;
                _iKeepInBuffer = 0;
            }
            catch { }
        }

        /// <summary>Zeromem buffers</summary>
        private void RealeaseBuffers()
        {
            fixed (byte* pbtOut = _btOutput, pbtIn = _btInput)
            {
                RtlZeroMemory(pbtOut, _iOutputLen);
                RtlZeroMemory(pbtIn, _iInputLen);
            }
        }
        #endregion

        #region Stream
        /// <summary>Read converted data into a byte array</summary>
        /// <param name="buffer">buffer</param>
        /// <param name="nsize">size of buffer</param>
        /// <param name="nread">bytes read</param>
        /// <returns>SND_RESULT</returns>
        private SND_RESULT StreamRead(ref byte[] buffer, uint nsize, ref uint nread)
        {
            nread = 0;
            SND_RESULT ret;
            do
            {
                // pcm buffer empty
                if (_iBufferData == 0)
                {
                    ret = FillBuffer();
                    if (ret == SND_RESULT.SND_ERR_END_OF_STREAM)
                        return ret;
                }
                // not enough data in the pcm buffer
                else if ((_iBufferData - _iPosInBuffer) < (nsize - nread))
                {
                    if ((_iBufferData - _iPosInBuffer) > 0)
                    {
                        Buffer.BlockCopy(_btOutput, (int)_iPosInBuffer, buffer, (int)nread, (int)(_iBufferData - _iPosInBuffer));
                        nread += (_iBufferData - _iPosInBuffer);
                    }
                    // end
                    ret = FillBuffer();
                    if (ret == SND_RESULT.SND_ERR_END_OF_STREAM)
                        return ret;
                }
                // enough data in the pcm buffer
                else
                {
                    Buffer.BlockCopy(_btOutput, (int)_iPosInBuffer, buffer, (int)nread, (int)(nsize - nread));
                    _iPosInBuffer += (nsize - nread);
                    nread += (nsize - nread);
                }
            } while (nread < nsize);

            _iFilePositionMS += (_tWFXOut.nAvgBytesPerSec * 100) / nread;

            return SND_RESULT.SND_ERR_SUCCESS;
        }

        /// <summary>Fill the input buffer and call convert</summary>
        /// <returns>bool</returns>
        private SND_RESULT FillBuffer()
        {
            uint nread = 0;
            uint nwritten = 0;
            SND_RESULT ret;

            if (_bInEndOfStream)
            {
                _iBufferData = 0;
                _iPosInBuffer = 0;
                RealeaseBuffers();
                return SND_RESULT.SND_ERR_END_OF_STREAM;
            }

            // get data from wav
            fixed (byte* pBt = &_btInput[_iKeepInBuffer])
            { ret = ReadWAVData(pBt, _iInputLen - _iKeepInBuffer, ref nread); }

            // either read error or end of file
            if (ret != SND_RESULT.SND_ERR_SUCCESS)
                _bInEndOfStream = true;

            Convert(nread + _iKeepInBuffer, _iOutputLen, ref nread, ref nwritten, _bInEndOfStream);

            _iPosInBuffer = 0;
            _iBufferData = nwritten;

            return ret;
        }

        /// <summary>Return wave header</summary>
        /// <returns>WAVEFORMATEX</returns>
        public WAVEFORMATEX GetHeader()
        {
            return _tWFXOut;
        }

        /// <summary>Converts data buffer to desired format</summary>
        /// <param name="inlen">source length</param>
        /// <param name="outlen">destination length</param>
        /// <param name="inused">length of source buffer used</param>
        /// <param name="outused">length of destination buffer used</param>
        /// <param name="last">last conversion cycle</param>
        /// <returns>MMSYSERR</returns>
        private MMSYSERR Convert(uint inlen, uint outlen, ref uint inused, ref uint outused, bool last)
        {
            uint lngFlags;
            ACMSTREAMHEADER udtHdr = new ACMSTREAMHEADER();

            lngFlags = ACM_STREAMCONVERTF_BLOCKALIGN;

            if (_bInFirst)
                lngFlags = lngFlags | ACM_STREAMCONVERTF_START;
            if (last)
                lngFlags = lngFlags | ACM_STREAMCONVERTF_END;
            MMSYSERR mmr;

            fixed (byte* pbtIn = _btInput, pbtOut = _btOutput)
            {
                {
                    udtHdr.cbStruct = (uint)Marshal.SizeOf(udtHdr);
                    udtHdr.cbSrcLength = inlen;
                    udtHdr.cbDstLength = outlen;
                    udtHdr.pbDst = pbtOut;
                    udtHdr.pbSrc = pbtIn;
                    mmr = acmStreamPrepareHeader(_hStream, ref udtHdr, 0);

                    if (mmr == MMSYSERR.NOERROR)
                    {
                        _bInFirst = false;
                        mmr = acmStreamConvert(_hStream, ref udtHdr, lngFlags);
                    }
                    if (mmr == MMSYSERR.NOERROR)
                    {
                        inused = (uint)udtHdr.cbSrcLengthUsed;
                        outused = (uint)udtHdr.cbDstLengthUsed;
                        _iKeepInBuffer = (uint)(udtHdr.cbSrcLength - udtHdr.cbSrcLengthUsed);

                        if (_iKeepInBuffer > 0)
                            // codec didn't use all the input bytes,
                            // move them to the first index of the input buffer
                            // to decode them with the next call to convert()
                            fixed (byte* pbtFt = &_btInput[0], pbtIn2 = &_btInput[(inlen - _iKeepInBuffer)])
                            { RtlMoveMemory(pbtFt, pbtIn2, _iKeepInBuffer); }
                    }
                    acmStreamUnprepareHeader(_hStream, ref udtHdr, 0);
                }
                return mmr;
            }
        }

        /// <summary>Reads data from file into a buffer</summary>
        /// <param name="pdata">buffer pointer</param>
        /// <param name="datalen">length to read</param>
        /// <param name="nread">length read</param>
        /// <returns>SND_RESULT</returns>
        private SND_RESULT ReadWAVData(byte* pdata, uint datalen, ref uint nread)
        {
            if (_waveHandle == INVALID_HANDLE)
                return SND_RESULT.SND_ERR_INVALID_SOURCE;

            if (FilePosition(_waveHandle) > (_ckData.Start + _ckData.Length))
            {
                // end of file reached
                nread = 0;
                return SND_RESULT.SND_ERR_END_OF_STREAM;
            }

            if (FilePosition(_waveHandle) + datalen > (_ckData.Start + _ckData.Length))
            {
                // almost at the end of the file, reached after this read
                datalen = (_ckData.Start + _ckData.Length) - FilePosition(_waveHandle);
                nread = FileRead(_waveHandle, pdata, datalen);
                return SND_RESULT.SND_ERR_END_OF_STREAM;
            }
            nread = FileRead(_waveHandle, pdata, datalen);
            return SND_RESULT.SND_ERR_SUCCESS;
        }
        #endregion

        #region Helpers
        /// <summary>Get the named position in a RIFF</summary>
        /// <param name="file">file name</param>
        /// <param name="name">named position</param>
        /// <returns>CHUNKINFO</returns>
        private CHUNKINFO GetChunkPos(string file, string name)
        {
            int ret;
            IntPtr hMmioIn;
            CHUNKINFO ck = new CHUNKINFO();
            MMCKINFO mmckinfoParentIn = new MMCKINFO();
            MMCKINFO mmckinfoSubchunkIn = new MMCKINFO();

            // open for read
            hMmioIn = mmioOpen(file, IntPtr.Zero, MMIO_READ);
            if (hMmioIn == IntPtr.Zero)
                return ck;

            // convert string to code
            mmckinfoParentIn.fccType = mmioStringToFOURCC("WAVE", 0);
            ret = mmioDescendParent(hMmioIn, &mmckinfoParentIn, IntPtr.Zero, MMIO_FINDRIFF);

            if (ret != 0)
            {
                mmioClose(hMmioIn, 0);
                return ck;
            }
            // get named location
            mmckinfoSubchunkIn.ckid = mmioStringToFOURCC(name, 0);
            ret = mmioDescend(hMmioIn, &mmckinfoSubchunkIn, &mmckinfoParentIn, MMIO_FINDCHUNK);

            if (ret != 0)
            {
                mmioClose(hMmioIn, 0);
                return ck;
            }
            // return position and size
            ck.Start = (uint)mmioSeek(hMmioIn, 0, SEEK_CUR);
            ck.Length = mmckinfoSubchunkIn.ckSize;
            mmioClose(hMmioIn, 0);
            return ck;
        }

        /// <summary>Create the WAVEFORMATEX structure</summary>
        /// <param name="sps">samples per second</param>
        /// <param name="chs">number of channels</param>
        /// <param name="bps">bits per sample</param>
        /// <returns>WAVEFORMATEX</returns>
        private WAVEFORMATEX CreateWFX(uint sps, ushort chs, ushort bps)
        {
            WAVEFORMATEX wf = new WAVEFORMATEX();
            wf.wFormatTag = WAVE_FORMAT_PCM;
            wf.nChannels = chs;
            wf.nSamplesPerSec = sps;
            wf.wBitsPerSample = bps;
            wf.nBlockAlign = (ushort)(chs * (bps / 8));
            wf.nAvgBytesPerSec = sps * wf.nBlockAlign;
            return wf;
        }
        #endregion
        #endregion
    }
}