using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncoder
{
    class Utilities
    {
        public static byte[] ConvertIntToByteArray(int I)
        {
            var result = BitConverter.GetBytes(I);

            if(result.Length != 4)
            {
                throw new InvalidOperationException("Can't have ints that aren't 4 bytes");
            }

            return result;
        }





        public static int ConvertByteArrayToInt32(byte[] b)
        {
            if (b.Length != 4)
            {
                throw new InvalidOperationException("Can't have ints that aren't 4 bytes");
            }

            return BitConverter.ToInt32(b, 0);
        }
    }

    public static class ExtensionMethods
    {
        private static Dictionary<byte, byte> MyByteDictionary = new Dictionary<byte, byte>();

        public static byte RoundByte(this byte i, int bitsToCompressTo)
        {
            if (bitsToCompressTo > 7)
            {
                // if more than 7 bits, the byte will remain the same
                return i;
            }

            if (MyByteDictionary.ContainsKey(i))
            {
                return MyByteDictionary[i];
            }
            else
            {
                int bits = bitsToCompressTo;
                int possibleValues = Convert.ToInt32(Math.Pow(2, bits));
                int MaxPossibleValues = 256;

                int roundNumber = MaxPossibleValues / possibleValues;

                byte result;
                if (i >= byte.MaxValue - roundNumber)
                {
                    result = (byte)(byte.MaxValue - roundNumber);
                }
                else
                {
                    var roundAnswer = ((int)Math.Round(i / roundNumber * 1.0)) * roundNumber;
                    result = Convert.ToByte(roundAnswer);
                }

                MyByteDictionary.Add(i, result);
                return result;
            }
        }

        public static byte CompressByte(this byte i, int bitsToCompressTo)
        {
            if(bitsToCompressTo > 4)
            {
                // if more than 4 bits, the byte will remain the same
                return i;
            }

            int bits = bitsToCompressTo;
            int possibleValues = Convert.ToInt32(Math.Pow(2, bits));
            int MaxPossibleValues = 256;

            int roundNumber = MaxPossibleValues / possibleValues;

            byte unroundedAnswer;
            if(i >= byte.MaxValue - roundNumber)
            {
                unroundedAnswer = (byte)(byte.MaxValue - roundNumber);
            }
            else
            {
                var roundAnswer = ((int)Math.Round(i / roundNumber * 1.0)) * roundNumber;
                unroundedAnswer = Convert.ToByte(roundAnswer);
            }

            return Convert.ToByte(unroundedAnswer / roundNumber);
        }
    }
}
