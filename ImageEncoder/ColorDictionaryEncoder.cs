using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncoder
{
    class ColorDictionaryEncoder
    {
        private Dictionary<int, int> _ColorCountDictionary;
        private Dictionary<byte, int> _IntValueToByteValuesDictionary;
        private Dictionary<int, byte> _ByteValueToIntValueDictionary;

        private byte _BitsToCompressTo = 8;

        public ColorDictionaryEncoder(int compressionRate)
        {
            var compressionRateDecimal = compressionRate / 100.0;
            byte _BitsToCompressTo = (byte)Math.Floor(compressionRateDecimal * 8);
            _BitsToCompressTo = Math.Max((byte)1, _BitsToCompressTo);
            _BitsToCompressTo = Math.Min((byte)4, _BitsToCompressTo);

            _ColorCountDictionary = new Dictionary<int, int>();
            _IntValueToByteValuesDictionary = new Dictionary<byte, int>();
            _ByteValueToIntValueDictionary = new Dictionary<int, byte>();
        }

        public void AddEntry(byte a, byte b, byte g, byte r)
        {
            a = a.RoundByte(_BitsToCompressTo);
            b = b.RoundByte(_BitsToCompressTo);
            g = g.RoundByte(_BitsToCompressTo);
            r = r.RoundByte(_BitsToCompressTo);

            int key = BitConverter.ToInt32(new byte[] { a, b, g, r }, 0);
            if (_ColorCountDictionary.ContainsKey(key))
            {
                _ColorCountDictionary[key] = _ColorCountDictionary[key] + 1;
            }
            else
            {
                _ColorCountDictionary[key] = 1;
            }
        }

        public List<byte> GetDictionaryByteValues()
        {
            _IntValueToByteValuesDictionary = new Dictionary<byte, int>();
            List<byte> bytesToWrite = new List<byte>();
            var _colorList = _ColorCountDictionary.OrderByDescending(x => x.Value).ToList();

            if(_colorList.Count > 256)
            {
                Console.WriteLine("Warning more than 256 colors found. " + _colorList.Count + " colors found");
            }

            var shortenedList = _colorList.Take(Byte.MaxValue).Select(x => x.Key).ToList();

            while(shortenedList.Count() < 255)
            {
                shortenedList.Add(0);
            }

            var shortenedArray = shortenedList.ToArray();

            for (int idx = 0 ; idx < shortenedArray.Length ; idx++)
            {
                int colorValueAsInt = shortenedArray[idx];
                _IntValueToByteValuesDictionary.Add((byte)idx, colorValueAsInt);
                if(!_ByteValueToIntValueDictionary.ContainsKey(colorValueAsInt))
                {
                    _ByteValueToIntValueDictionary.Add(colorValueAsInt, (byte)idx);
                }

                byte[] colorBytes = BitConverter.GetBytes(colorValueAsInt);

                bytesToWrite.Add((byte)idx);
                bytesToWrite.Add(colorBytes[0]);
                bytesToWrite.Add(colorBytes[1]);
                bytesToWrite.Add(colorBytes[2]);
                bytesToWrite.Add(colorBytes[3]);
            }

            return bytesToWrite;
        }

        internal byte GetBytesForColors(byte a, byte b, byte g, byte r)
        {
            a = a.RoundByte(_BitsToCompressTo);
            b = b.RoundByte(_BitsToCompressTo);
            g = g.RoundByte(_BitsToCompressTo);
            r = r.RoundByte(_BitsToCompressTo);
            int key = BitConverter.ToInt32(new byte[] { a, b, g, r }, 0);
            if(_ByteValueToIntValueDictionary.ContainsKey(key))
            { 
                byte result = _ByteValueToIntValueDictionary[key];
                return result;
            }
            else
            {
                return _ByteValueToIntValueDictionary.First().Value;
            }
        }

        private Dictionary<byte, Color> _KeyToValuesDictionary = null;
        public void AddDecoderEntry(byte key, byte a, byte b, byte g, byte r)
        {
            if(_KeyToValuesDictionary == null)
            {
                _KeyToValuesDictionary = new Dictionary<byte, Color>();
            }
            _KeyToValuesDictionary.Add(key, System.Drawing.Color.FromArgb(a,r,g,b));
        }

        public Color GetColorFromByte(byte input)
        {
            return _KeyToValuesDictionary[input];
        }
    }
}
