#region About
// public domain C files
// http://www.musicdsp.org/files/biquad.c
// Simple implementation of Biquad filters -- Tom St Denis
// Based on the work
// Cookbook formulae for audio EQ biquad filter coefficients
// by Robert Bristow-Johnson, pbjrbj@viconet.com  a.k.a. robert@audioheads.com
// Available on the web at
// http://www.smartelectronix.com
// helpful -(I hope you like math!)
// http://www.musicdsp.org/archive.php?classid=3#276
#endregion

#region Directives
using System;
using System.Runtime.InteropServices;
#endregion

namespace Personal_Wave_Recorder
{
    #region Structs
    public struct biquad
    {
        public double a0, a1, a2, a3, a4;
        public double x1, x2, y1, y2;
    };
    #endregion

    unsafe class IIR
    {
        #region Enums
        /// <summary>Filter type</summary>
        public enum Filter
        {
            /// <summary>low pass filter</summary>
            LPF,
            /// <summary>high pass filter</summary>
            HPF,
            /// <summary>band pass filter</summary>
            BPF,
            /// <summary>notch Filter</summary>
            NOTCH,
            /// <summary>peaking band eq filter</summary>
            PEQ,
            /// <summary>low shelf filter</summary>
            LSH,
            /// <summary>high shelf filter</summary>
            HSH
        };
        #endregion

        #region Constants
        private const double M_PI = 3.14159265358979323846;
        private const double M_LN2 = 0.69314718055994530942;
        private const int HEAP_ZERO_MEMORY = 0x00000008;
        #endregion

        #region API
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern biquad* HeapAlloc(IntPtr hHeap, uint dwFlags, uint dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool HeapFree(IntPtr hHeap, uint dwFlags, biquad* lpMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetProcessHeap();
        #endregion

        #region Memory
        /// <summary>Allocate heap memory</summary>
        /// <param name="size">size desired</param>
        /// <returns>memory address</returns>
        private biquad* Alloc(int size)
        {
            return HeapAlloc(GetProcessHeap(), HEAP_ZERO_MEMORY, (uint)size);
        }

        /// <summary>Release heap memory</summary>
        /// <param name="pmem">memory address</param>
        private void Free(biquad* b)
        {
            HeapFree(GetProcessHeap(), 0, b);
        }

        public void FreeMem(biquad* b)
        {
            Free(b); 
        }
        #endregion

        #region Biquad
        /// <summary>Calculates IIR</summary>
        /// <param name="sample">sample rate</param>
        /// <param name="b">biquad pointer</param>
        public void BiQuad(ref Int16 sample, biquad* b)
        {
            // compute result
            double result = b->a0 * sample + b->a1 * b->x1 + b->a2 * b->x2 - b->a3 * b->y1 - b->a4 * b->y2;
            // shift x1 to x2, sample to x1
            b->x2 = b->x1;
            b->x1 = sample;
            // shift y1 to y2, result to y1
            b->y2 = b->y1;
            b->y1 = result;

            sample = (Int16)result;
        }

        public void BiQuad(ref byte sample, biquad* b)
        {
            // compute result
            double result = b->a0 * sample + b->a1 * b->x1 + b->a2 * b->x2 - b->a3 * b->y1 - b->a4 * b->y2;
            // shift x1 to x2, sample to x1
            b->x2 = b->x1;
            b->x1 = sample;
            // shift y1 to y2, result to y1
            b->y2 = b->y1;
            b->y1 = result;

            sample = (byte)result;
        }

        /// <summary>Set up a BiQuad Filter</summary>
        /// <param name="type">filter type</param>
        /// <param name="dbGain">gain of filter</param>
        /// <param name="freq">center frequency</param>
        /// <param name="srate">sampling rate</param>
        /// <param name="bandwidth">bandwidth in octaves</param>
        /// <returns>biquad pointer</returns>
        public biquad* BiQuadFilter(Filter type, double dbGain, double freq, double srate, double bandwidth)
        {
            biquad* b;
            double A, omega, sn, cs, alpha, beta;
            double a0, a1, a2, b0, b1, b2;

            b = Alloc(sizeof(biquad));
            if (b == null)
                return null;

            // setup variables
            A = Math.Pow(10, dbGain / 40);
            omega = 2 * M_PI * freq / srate;
            sn = Math.Sin(omega);
            cs = Math.Cos(omega);
            alpha = sn * Math.Sinh(M_LN2 / 2 * bandwidth * omega / sn);
            beta = Math.Sqrt(A + A);

            switch (type)
            {
                case Filter.LPF:
                    b0 = (1 - cs) / 2;
                    b1 = 1 - cs;
                    b2 = (1 - cs) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * cs;
                    a2 = 1 - alpha;
                    break;
                case Filter.HPF:
                    b0 = (1 + cs) / 2;
                    b1 = -(1 + cs);
                    b2 = (1 + cs) / 2;
                    a0 = 1 + alpha;
                    a1 = -2 * cs;
                    a2 = 1 - alpha;
                    break;
                case Filter.BPF:
                    b0 = alpha;
                    b1 = 0;
                    b2 = -alpha;
                    a0 = 1 + alpha;
                    a1 = -2 * cs;
                    a2 = 1 - alpha;
                    break;
                case Filter.NOTCH:
                    b0 = 1;
                    b1 = -2 * cs;
                    b2 = 1;
                    a0 = 1 + alpha;
                    a1 = -2 * cs;
                    a2 = 1 - alpha;
                    break;
                case Filter.PEQ:
                    b0 = 1 + (alpha * A);
                    b1 = -2 * cs;
                    b2 = 1 - (alpha * A);
                    a0 = 1 + (alpha / A);
                    a1 = -2 * cs;
                    a2 = 1 - (alpha / A);
                    break;
                case Filter.LSH:
                    b0 = A * ((A + 1) - (A - 1) * cs + beta * sn);
                    b1 = 2 * A * ((A - 1) - (A + 1) * cs);
                    b2 = A * ((A + 1) - (A - 1) * cs - beta * sn);
                    a0 = (A + 1) + (A - 1) * cs + beta * sn;
                    a1 = -2 * ((A - 1) + (A + 1) * cs);
                    a2 = (A + 1) + (A - 1) * cs - beta * sn;
                    break;
                case Filter.HSH:
                    b0 = A * ((A + 1) + (A - 1) * cs + beta * sn);
                    b1 = -2 * A * ((A - 1) + (A + 1) * cs);
                    b2 = A * ((A + 1) + (A - 1) * cs - beta * sn);
                    a0 = (A + 1) - (A - 1) * cs + beta * sn;
                    a1 = 2 * ((A - 1) - (A + 1) * cs);
                    a2 = (A + 1) - (A - 1) * cs - beta * sn;
                    break;
                default:
                    Free(b);
                    return null;
            }

            // precompute the coefficients
            b->a0 = b0 / a0;
            b->a1 = b1 / a0;
            b->a2 = b2 / a0;
            b->a3 = a1 / a0;
            b->a4 = a2 / a0;
            // zero initial samples
            b->x1 = b->x2 = 0;
            b->y1 = b->y2 = 0;

            return b;
        }
        #endregion
    }
}
