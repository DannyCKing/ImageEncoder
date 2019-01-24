using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageEncoder
{
    public partial class Form1 : Form
    {
        private string _ImportImageFilePath;

        private string _OutputTextFilePath;

        private string _ImportTextFilePath;

        private string _OutputImageFilePath;

        private string EXTENSION = "txt";

        private ColorDictionaryEncoder _ColorDictionary;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                var initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.gif, *.bmp, *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png";
                //openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    _ImportImageFilePath = filePath;
                    inputFileTextBox.Text = _ImportImageFilePath;

                    var defaultOutputDirectory = Path.GetDirectoryName(_ImportImageFilePath);
                    var defaultOutputName = Path.GetFileNameWithoutExtension(_ImportImageFilePath);
                    var outputPath = Path.Combine(defaultOutputDirectory.ToString(), defaultOutputName + "_translated." + EXTENSION);
                    _OutputTextFilePath = outputPath;
                    outputFileTextBox.Text = _OutputTextFilePath;

                    SetImportTextFilePathAndOutputImagePath(_OutputTextFilePath);

                    //this.inputTextFilePathTextBox.Text = _OutputTextFilePath;
                    //var imageFileDefaultOutputName = Path.GetFileNameWithoutExtension(_OutputTextFilePath);
                    //var outputImagePath = Path.Combine(defaultOutputDirectory.ToString(), imageFileDefaultOutputName + "_utranslated");
                    //this.outputImageFilePathTextBox.Text = Path.Combine(defaultOutputDirectory.ToString(), defaultOutputName + "_untranslated");
                    //_ImportTextFilePath = inputTextFilePathTextBox.Text;
                    //_OutputImageFilePath = outputImageFilePathTextBox.Text;

                    ////Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
        }

        private void convertImageToTextButton_Click(object sender, EventArgs eventArgs)
        {
            convertImageToTextButton_Click2(sender, eventArgs);
            return;
            bool doEncryption = false;
            string tempFilePath = "";
            Bitmap img;

            if (string.IsNullOrWhiteSpace(_ImportImageFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (compressCheckBox.Checked)
            {
                // compress if checked
                try
                {
                    // get temp folder output path
                    var importPath = Path.GetDirectoryName(_ImportImageFilePath);
                    var tempFileName = Path.GetFileNameWithoutExtension(_ImportImageFilePath) + Guid.NewGuid().ToString() + Path.GetExtension(_ImportImageFilePath);
                    var newFilePath = Path.Combine(importPath, tempFileName);
                    tempFilePath = newFilePath;

                    Encoder.CompressImage(_ImportImageFilePath, newFilePath, compressionRateTrackBar.Value);
                    _ImportImageFilePath = newFilePath;
                }
                catch(Exception e)
                {
                    //unable to compress, ignore
                }
            }

            try
            {
                img = new Bitmap(_ImportImageFilePath);
            }
            catch (Exception bitmapException)
            {
                MessageBox.Show(this, "Invalid Image Type", "Unsupported image type.  Try another file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (img.Width > short.MaxValue)
            {
                MessageBox.Show(this, "Image Too Large", "Image is too large(too wide).  Try another file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (img.Height > short.MaxValue)
            {
                MessageBox.Show(this, "Image Too Large", "Image is too large(too tall).  Try another file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(string.IsNullOrWhiteSpace(_OutputTextFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<byte> bytesToWriteToFile = new List<byte>();

            // first byte contains whether it's encrpyed or not
            byte isEncryptedByte = Convert.ToByte(doEncryption);

            bytesToWriteToFile.Add(isEncryptedByte);

            // next 4 bytes contains the width
            var widthByteArray = Utilities.ConvertIntToByteArray(img.Width);
            bytesToWriteToFile.Add(widthByteArray[0]);
            bytesToWriteToFile.Add(widthByteArray[1]);
            bytesToWriteToFile.Add(widthByteArray[2]);
            bytesToWriteToFile.Add(widthByteArray[3]);

            // next four bytes contains the height
            var heightByteArray = Utilities.ConvertIntToByteArray(img.Height);
            bytesToWriteToFile.Add(heightByteArray[0]);
            bytesToWriteToFile.Add(heightByteArray[1]);
            bytesToWriteToFile.Add(heightByteArray[2]);
            bytesToWriteToFile.Add(heightByteArray[3]);

            var imageExtension = Path.GetExtension(_ImportImageFilePath);
            ImageType imageTypeEnum = ImageTypeDictionary.GetEnumFromString(imageExtension);
            byte imageType = ImageTypeDictionary.GetIntFromImageType(imageTypeEnum);
            bytesToWriteToFile.Add(imageType);

            // add ignore alpha channel byte to file
            byte ignoreAlpha = Convert.ToByte(ignoreAlphaChannelCheckbox.Checked);
            bytesToWriteToFile.Add(ignoreAlpha);

            //next byte is the compression rate
            var compressionRateDecimal = compressionRateTrackBar.Value / 100.0;
            byte bitsToCompressTo = (byte)Math.Floor(compressionRateDecimal * 8);
            if (bitsToCompressTo == 0)
            {
                bitsToCompressTo = 1;
            }
            // not adding compression rate right now
            //bytesToWriteToFile.Add(bitsToCompressTo);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);
                    
                    byte a = pixel.A;
                    byte b = pixel.B;
                    byte g = pixel.G;
                    byte r = pixel.R;

                    bytesToWriteToFile.Add(b);
                    bytesToWriteToFile.Add(g);
                    bytesToWriteToFile.Add(r);
                    if (!ignoreAlphaChannelCheckbox.Checked)
                    {
                        bytesToWriteToFile.Add(a);
                    }

                    _ColorDictionary.AddEntry(a, b, g, r);
                }

                int progress = (int) ((i * 1.0 / img.Width) * 100);
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + " %";
                progressLabel.Refresh();
            }

            var test = _ColorDictionary.GetDictionaryByteValues();
            var uncompressed = bytesToWriteToFile.ToArray();
            var compressed = Compression.Compress(uncompressed);
            File.WriteAllBytes(_OutputTextFilePath, compressed); // Requires System.IO

            progressBar.Value = 0;
            progressLabel.Text = "Done";

            _ImportImageFilePath = inputFileTextBox.Text;

            if(!string.IsNullOrEmpty(tempFilePath))
            {
                //File.Delete(tempFilePath);
            }
        }


        private void convertImageToTextButton_Click2(object sender, EventArgs eventArgs)
        {
            _ColorDictionary = new ColorDictionaryEncoder(compressionRateTrackBar.Value);
            bool doEncryption = false;
            string tempFilePath = "";
            Bitmap img;

            if (string.IsNullOrWhiteSpace(_ImportImageFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (compressCheckBox.Checked)
            {
                // compress if checked
                try
                {
                    // get temp folder output path
                    var importPath = Path.GetDirectoryName(_ImportImageFilePath);
                    var tempFileName = Path.GetFileNameWithoutExtension(_ImportImageFilePath) + Guid.NewGuid().ToString() + Path.GetExtension(_ImportImageFilePath);
                    var newFilePath = Path.Combine(importPath, tempFileName);
                    tempFilePath = newFilePath;

                    Encoder.CompressImage(_ImportImageFilePath, newFilePath, compressionRateTrackBar.Value);
                    _ImportImageFilePath = newFilePath;
                }
                catch (Exception e)
                {
                    //unable to compress, ignore
                }
            }

            try
            {
                img = new Bitmap(_ImportImageFilePath);
            }
            catch (Exception bitmapException)
            {
                MessageBox.Show(this, "Invalid Image Type", "Unsupported image type.  Try another file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (img.Width > short.MaxValue)
            {
                MessageBox.Show(this, "Image Too Large", "Image is too large(too wide).  Try another file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (img.Height > short.MaxValue)
            {
                MessageBox.Show(this, "Image Too Large", "Image is too large(too tall).  Try another file.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(_OutputTextFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<byte> bytesToWriteToFile = new List<byte>();

            // first byte contains whether it's encrpyed or not
            byte isEncryptedByte = Convert.ToByte(doEncryption);

            bytesToWriteToFile.Add(isEncryptedByte);

            // next 4 bytes contains the width
            var widthByteArray = Utilities.ConvertIntToByteArray(img.Width);
            bytesToWriteToFile.Add(widthByteArray[0]);
            bytesToWriteToFile.Add(widthByteArray[1]);
            bytesToWriteToFile.Add(widthByteArray[2]);
            bytesToWriteToFile.Add(widthByteArray[3]);

            // next four bytes contains the height
            var heightByteArray = Utilities.ConvertIntToByteArray(img.Height);
            bytesToWriteToFile.Add(heightByteArray[0]);
            bytesToWriteToFile.Add(heightByteArray[1]);
            bytesToWriteToFile.Add(heightByteArray[2]);
            bytesToWriteToFile.Add(heightByteArray[3]);

            var imageExtension = Path.GetExtension(_ImportImageFilePath);
            ImageType imageTypeEnum = ImageTypeDictionary.GetEnumFromString(imageExtension);
            byte imageType = ImageTypeDictionary.GetIntFromImageType(imageTypeEnum);
            bytesToWriteToFile.Add(imageType);

            // add ignore alpha channel byte to file
            byte ignoreAlpha = Convert.ToByte(ignoreAlphaChannelCheckbox.Checked);
            bytesToWriteToFile.Add(ignoreAlpha);

            //next byte is the compression rate
            var compressionRateDecimal = compressionRateTrackBar.Value / 100.0;
            byte bitsToCompressTo = (byte)Math.Floor(compressionRateDecimal * 8);
            if (bitsToCompressTo == 0)
            {
                bitsToCompressTo = 1;
            }
            // not adding compression rate right now
            //bytesToWriteToFile.Add(bitsToCompressTo);

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);

                    byte a = pixel.A;
                    byte b = pixel.B;
                    byte g = pixel.G;
                    byte r = pixel.R;
                    _ColorDictionary.AddEntry(a, b, g, r);
                }

                int progress = (int)((i * 1.0 / img.Width) * 100);
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + " %";
                progressLabel.Refresh();
            }

            bytesToWriteToFile.AddRange(_ColorDictionary.GetDictionaryByteValues());

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Color pixel = img.GetPixel(i, j);

                    byte a = pixel.A;
                    byte b = pixel.B;
                    byte g = pixel.G;
                    byte r = pixel.R;
                    byte byteToWrite = _ColorDictionary.GetBytesForColors(a, b, g, r);
                    bytesToWriteToFile.Add(byteToWrite);
                }

                int progress = (int)((i * 1.0 / img.Width) * 100);
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + " %";
                progressLabel.Refresh();
            }

            var uncompressed = bytesToWriteToFile.ToArray();
            var compressed = Compression.Compress(uncompressed);
            File.WriteAllBytes(_OutputTextFilePath, compressed); // Requires System.IO

            progressBar.Value = 0;
            progressLabel.Text = "Done";

            _ImportImageFilePath = inputFileTextBox.Text;

            if (!string.IsNullOrEmpty(tempFilePath))
            {
                img.Dispose();
                //File.Delete(tempFilePath);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            compressCheckBox_CheckedChanged(this, null);
        }

        private void chooseInputTextFilePathButton_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                var initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.InitialDirectory = initialDirectory;
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //openFileDialog.FilterIndex = 2;
                //openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    SetImportTextFilePathAndOutputImagePath(filePath);
                    //inputTextFilePathTextBox.Text = _ImportTextFilePath;

                    //var defaultOutputDirectory = Path.GetDirectoryName(_ImportTextFilePath);
                    //var defaultOutputName = Path.GetFileNameWithoutExtension(_ImportTextFilePath);
                    //var outputPath = Path.Combine(defaultOutputDirectory.ToString(), defaultOutputName + "_untranslated");
                    //_OutputImageFilePath = outputPath;
                    //outputImageFilePathTextBox.Text = _OutputImageFilePath;
                }
            }
        }

        private void SetImportTextFilePathAndOutputImagePath(string input)
        {
            _ImportTextFilePath = input;
            inputTextFilePathTextBox.Text = input;

            var defaultOutputDirectory = Path.GetDirectoryName(_ImportTextFilePath);
            var defaultOutputName = Path.GetFileNameWithoutExtension(_ImportTextFilePath);
            var outputPath = Path.Combine(defaultOutputDirectory.ToString(), defaultOutputName + "_untranslated");
            _OutputImageFilePath = outputPath;
            outputImageFilePathTextBox.Text = _OutputImageFilePath;
        }

        private void convertFromTextToImageFileButton_Click(object sender, EventArgs e)
        {
            convertFromTextToImageFileButton_Click2(sender, e);
            return;
            if (string.IsNullOrWhiteSpace(_ImportTextFilePath))
            {
                MessageBox.Show(this, "No input path", "Choose a file to input to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_OutputImageFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var compressedByteArray = File.ReadAllBytes(_ImportTextFilePath);

            var inputByteArray = Compression.Decompress(compressedByteArray);

            //First byte is if it's encrypted
            bool isEncrypted = Convert.ToBoolean(inputByteArray[0]);

            // next 4 bytes contains the width
            int width = Utilities.ConvertByteArrayToInt32(new byte[] { inputByteArray[1], inputByteArray[2], inputByteArray[3], inputByteArray[4] });

            // next 4 bytes contains the height
            int height = Utilities.ConvertByteArrayToInt32(new byte[] { inputByteArray[5], inputByteArray[6], inputByteArray[7], inputByteArray[8] });

            // next byte is image type
            ImageType imageType = ImageTypeDictionary.GetImageFromInt(inputByteArray[9]);

            bool ignoreAlpha = Convert.ToBoolean(inputByteArray[10]);

            _OutputImageFilePath = _OutputImageFilePath + "." + imageType.ToString();
            int currentIndex = 11;

            Bitmap outputImage = new Bitmap(width, height) ;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    byte b = inputByteArray[currentIndex + 0];
                    byte g = inputByteArray[currentIndex + 1];
                    byte r = inputByteArray[currentIndex + 2];
                    byte a = 255;

                    if (!ignoreAlpha)
                    {
                        a = inputByteArray[currentIndex + 3];
                    }
                    outputImage.SetPixel(i, j, Color.FromArgb(a, r, g, b));

                    if(ignoreAlpha)
                    {
                        currentIndex += 3;
                    }
                    else
                    {
                        currentIndex += 4;
                    }
                }

                int progress = (int)((i * 1.0 / width) * 100);
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + " %";
                progressLabel.Refresh();
            }

            progressBar.Value = 0;
            progressLabel.Text = "Done";

            outputImage.Save(_OutputImageFilePath);
        }

        private void convertFromTextToImageFileButton_Click2(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_ImportTextFilePath))
            {
                MessageBox.Show(this, "No input path", "Choose a file to input to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(_OutputImageFilePath))
            {
                MessageBox.Show(this, "No output path", "Choose a file to output to.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var compressedByteArray = File.ReadAllBytes(_ImportTextFilePath);

            var inputByteArray = Compression.Decompress(compressedByteArray);

            //First byte is if it's encrypted
            bool isEncrypted = Convert.ToBoolean(inputByteArray[0]);

            // next 4 bytes contains the width
            int width = Utilities.ConvertByteArrayToInt32(new byte[] { inputByteArray[1], inputByteArray[2], inputByteArray[3], inputByteArray[4] });

            // next 4 bytes contains the height
            int height = Utilities.ConvertByteArrayToInt32(new byte[] { inputByteArray[5], inputByteArray[6], inputByteArray[7], inputByteArray[8] });

            // next byte is image type
            ImageType imageType = ImageTypeDictionary.GetImageFromInt(inputByteArray[9]);

            bool ignoreAlpha = Convert.ToBoolean(inputByteArray[10]);

            _OutputImageFilePath = _OutputImageFilePath + "." + imageType.ToString();

            int currentIndex = 11;

            _ColorDictionary = new ColorDictionaryEncoder(8);

            // bytes 11 - 1285 contain the byte dictionary library
            for (int i = 0; i < Byte.MaxValue; i++)
            {
                int currentDictionaryIndex = currentIndex + (i * 5);
                var key = inputByteArray[currentDictionaryIndex + 0];
                var a = inputByteArray[currentDictionaryIndex + 1];
                var b = inputByteArray[currentDictionaryIndex + 2];
                var g = inputByteArray[currentDictionaryIndex + 3];
                var r = inputByteArray[currentDictionaryIndex + 4];

                _ColorDictionary.AddDecoderEntry(key, a, b, g, r);
            }

            currentIndex = 1286;
            
            Bitmap outputImage = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    byte currentByte = inputByteArray[currentIndex];
                    Color outputColor = _ColorDictionary.GetColorFromByte(currentByte);

                    outputImage.SetPixel(i, j, outputColor);

                    currentIndex++;
                }

                int progress = (int)((i * 1.0 / width) * 100);
                progressBar.Value = progress;
                progressLabel.Text = progress.ToString() + " %";
                progressLabel.Refresh();
            }

            progressBar.Value = 0;
            progressLabel.Text = "Done";

            outputImage.Save(_OutputImageFilePath);
        }

        private void compressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            compressionLevelLabel.Enabled = compressCheckBox.Checked;
            compressionRateTrackBar.Enabled = compressCheckBox.Checked;
        }

        private void compressionRateTrackBar_Scroll(object sender, EventArgs e)
        {
            compressionLevelLabel.Text = compressionRateTrackBar.Value.ToString() + " %";
        }
    }
}
