using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncoder
{
    class Colors
    {
        internal static byte[] GetBytesForColors(byte alpha, byte blue, byte green, byte red, byte bitsToCompressTo)
        {
            byte[] returnResult = new byte []{ };

            byte a = alpha.CompressByte(bitsToCompressTo);
            byte b = blue.CompressByte(bitsToCompressTo);
            byte g = green.CompressByte(bitsToCompressTo);
            byte r = red.CompressByte(bitsToCompressTo);

            if (bitsToCompressTo > 4)
            {
                // greater than four bits, store in 8 bit blocks


            }
            else if (bitsToCompressTo > 2)
            {
                // if 3 or 4 bits, store in 4 bits
            }
            else if(bitsToCompressTo > 1)
            {
                // if 2 bits store in 2 bits
            }
            else
            {
               // if 1 bit, store 
            }


            return returnResult;
        }
    }
}
