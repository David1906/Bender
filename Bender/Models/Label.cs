using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Bender.Enums;
using Bender.Extensions;
using QRCoder;

namespace Bender.Models
{
    public class Label
    {
        public string Code { get; set; } = "";
        public List<LabelItem> Items = new List<LabelItem>();

        public Label(List<LabelItem> items)
        {
            this.Items = items;
            this.SetFormatIndexes();
        }

        public void Decode()
        {
            var codeCopy = this.Code;
            foreach (var item in this.Items)
            {
                var text = "";
                switch (item.Mode)
                {
                    case Modes.Decode:
                        text = this.DecodeValue(item, codeCopy);
                        if (codeCopy.Length > 0)
                        {
                            codeCopy = codeCopy.Remove(0, text.Length + item.Terminator.DefaultValue().Length);
                        }
                        break;
                    case Modes.Fixed:
                    case Modes.Scann:
                        text = item.Value;
                        break;
                }
                item.Value = text;
            }
        }
        public string Encode()
        {
            var enabledItems = this.Items.Where(x => !x.IsDisabled).ToList();
            var text = "";
            foreach (var item in enabledItems)
            {
                text += item.Value + item.Terminator.DefaultValue();
            }
            return text;
        }
        private string DecodeValue(LabelItem item, string text)
        {
            var terminatorIndex = text.IndexOf(item.Terminator.DefaultValue());
            if (!item.HasTerminator)
            {
                terminatorIndex = text.Length;
            }
            if (terminatorIndex > -1)
            {
                return text[..terminatorIndex];
            }
            return "";
        }
        public void SwapUp(LabelItem item)
        {
            var sourceIndex = this.Items.FindIndex(x => x == item);
            var targetIndex = sourceIndex - 1;
            if (sourceIndex >= 1 && targetIndex >= 0)
            {
                this.Items.Swap(sourceIndex, targetIndex);
            }
            this.SetFormatIndexes();
        }

        public void SwapDown(LabelItem item)
        {
            var sourceIndex = this.Items.FindIndex(x => x == item);
            var targetIndex = sourceIndex + 1;
            if (sourceIndex >= 0 && targetIndex < this.Items.Count)
            {
                this.Items.Swap(sourceIndex, targetIndex);
            }
            this.SetFormatIndexes();
        }

        private void SetFormatIndexes()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Items[i].Index = i + 1;
            }
        }
        public BitmapImage GenerateQRCode()
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(this.Encode(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            using MemoryStream memory = new MemoryStream();
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
}
