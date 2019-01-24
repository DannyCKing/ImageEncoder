using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEncoder
{
    class Encoder
    {
        public static Bitmap GetBitmap(string path)
        {
            Bitmap source = null;

            try
            {
                byte[] imageData = System.IO.File.ReadAllBytes(path);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(imageData, false);
                source = new Bitmap(stream);
            }
            catch { }

            return source;
        }


        public static void CompressImage(string fullFilePath, string destinationPath, long compression)
        {
            Bitmap bitmap = GetBitmap(fullFilePath);
            if (bitmap == null)
            {
                return;
            }

            Bitmap newImage = new Bitmap(bitmap);

            bool encoderFound = false;
            System.Drawing.Imaging.ImageCodecInfo encoder = null;

            var fileName = Path.GetFileName(fullFilePath);

            if (fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".jpeg"))
            {
                encoderFound = true;
                encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (fileName.ToLower().EndsWith(".bmp"))
            {
                encoderFound = true;
                encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else if (fileName.ToLower().EndsWith(".tiff"))
            {
                encoderFound = true;
                encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Tiff);
            }
            else if (fileName.ToLower().EndsWith(".gif"))
            {
                encoderFound = true;
                encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Gif);
            }
            else if (fileName.ToLower().EndsWith(".png"))
            {
                encoderFound = true;
                encoder = GetEncoder(System.Drawing.Imaging.ImageFormat.Png);
            }

            if (encoderFound)
            {
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                System.Drawing.Imaging.EncoderParameters myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);
                System.Drawing.Imaging.EncoderParameter myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, compression);
                myEncoderParameters.Param[0] = myEncoderParameter;
                
                newImage.Save(System.IO.Path.Combine(destinationPath), encoder, myEncoderParameters);
            }
            else
            {
                newImage.Save(System.IO.Path.Combine(destinationPath));
            }
        }


        public static ImageCodecInfo GetEncoder(string mimeType)
        {
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (int x = 0; x < encoders.Length; x++)
            {
                if (encoders[x].MimeType == mimeType)
                {
                    return encoders[x];
                }
            }
            return null;
        }


        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
