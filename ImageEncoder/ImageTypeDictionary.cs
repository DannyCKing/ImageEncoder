using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncoder
{
    public enum ImageType
    {
        bmp,
        gif,
        jpg,
        jpeg,
        png,
        tiff
    }

    public class ImageTypeDictionary
    {
        public static ImageType GetEnumFromString(string imageExtension)
        {
            imageExtension = imageExtension.Trim('.');
            ImageType returnValue;
            Enum.TryParse(imageExtension, out returnValue);
            return returnValue;
        }

        private static Dictionary<byte, ImageType> IntToImageType = new Dictionary<byte, ImageType>
        {
            {0, ImageType.bmp},
            {1, ImageType.gif },
            {2, ImageType.jpg },
            {3, ImageType.jpeg },
            {4, ImageType.png },
            {5, ImageType.tiff }
        };

        public static byte GetIntFromImageType(ImageType imagetype)
        {
            try
            {
                var keyValuePair = IntToImageType.FirstOrDefault(x => x.Value == imagetype);

                return keyValuePair.Key;
            }
            catch(Exception e)
            {
                return 0;
            }
        }

        public static ImageType GetImageFromInt(byte key)
        {
            return IntToImageType[key];
        }
    }
}
