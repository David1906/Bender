using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Bender.Models
{
    public class Code
    {
        private const string SEPARATOR = ",";

        private string _text = "";
        private string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value.Replace("'", "-");
            }
        }

        public Code(string text)
        {
            Text = text;
        }

        public PackageInfo Decode(Format format)
        {
            var packageInfo = new PackageInfo();
            var textCopy = this.Text;
            foreach (var chunk in format.Items)
            {
                var text = "";
                switch (chunk.Mode)
                {
                    case "Decode":
                        text = this.DecodeValue(chunk, textCopy);
                        break;
                    case "Fixed":
                    case "Scann":
                        text = chunk.Value;
                        break;
                }
                packageInfo.SetPropByName(chunk.Property, text);
                if (textCopy.Length > 0)
                {
                    textCopy = textCopy.Remove(0, text.Length + (chunk.HasTerminator ? chunk.Terminator.Length : 0)); // TODO: cambiar a enumerado
                }
            }
            return packageInfo;
        }
        public string DecodeValue(CodeChunk chunk, string text)
        {
            var terminatorIndex = text.IndexOf(chunk.Terminator);
            if (String.IsNullOrEmpty(chunk.Terminator) || !chunk.HasTerminator)
            {
                terminatorIndex = text.Length;
            }
            if (terminatorIndex > -1)
            {
                return text.Substring(0, terminatorIndex);
            }
            return "";
        }
        public BitmapImage GenerateQr(Format format)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(this.Encode(format), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using (MemoryStream memory = new MemoryStream())
            {
                qrCodeImage.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
        public string Encode(Format format)
        {
            var enabledItems = format.Items.Where(x => !x.IsDisabled).ToList();
            var text = "";
            foreach (var item in enabledItems)
            {
                if (!item.IsDisabled)
                {
                    text += item.Value;
                }
                if (item != enabledItems.Last())
                {
                    text += SEPARATOR;
                }
            }
            return text;
        }
    }
}
