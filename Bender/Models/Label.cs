using Bender.Enums;
using Bender.Extensions;
using QRCoder;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System;

namespace Bender.Models
{
    public class Label
    {
        public string Code { get; set; } = "";
        public string Name { get; set; }

        public BindingList<LabelItem> Items = new BindingList<LabelItem>();

        public Label(BindingList<LabelItem> items)
        {
            this.Items = items;
            this.SetFormatIndexes();
        }

        public void CopyValues(Label label)
        {
            foreach (var item in label.Items)
            {
                var myItem = this.Items.First(x => x.Key == item.Key);
                if (myItem != null)
                {
                    myItem.Value = item.Value;
                }
            }
            this.Items.ResetBindings();
        }
        public void Decode()
        {
            var codeCopy = this.Code.Replace("'", "-");
            foreach (var item in this.Items)
            {
                var text = "";
                switch (item.Mode)
                {
                    case Modes.Decode:
                        text = this.DecodeValue(item, codeCopy);
                        var textEnd = text.Length + item.Terminator.DefaultValue().Length;
                        if (codeCopy.Length > 0 && textEnd <= codeCopy.Length)
                        {
                            codeCopy = codeCopy.Remove(0, textEnd);
                        }
                        break;
                    case Modes.Fixed:
                        text = item.Value;
                        break;
                    case Modes.Scann:
                        text = "";
                        break;
                }
                item.Value = text;
            }
            this.Items.ResetBindings();
        }
        public string Encode()
        {
            var enabledItems = this.Items.Where(x => !x.IsDisabled).ToList();
            this.Code = "";
            foreach (var item in enabledItems)
            {
                this.Code += item.Value + item.Terminator.DefaultValue();
            }
            return this.Code;
        }
        private string DecodeValue(LabelItem item, string text)
        {
            var terminatorIndex = text.IndexOf(item.Terminator.DefaultValue());
            if (!item.HasTerminator || terminatorIndex < 0)
            {
                terminatorIndex = text.Length;
            }
            if (terminatorIndex <= text.Length)
            {
                return text[..terminatorIndex];
            }
            return "";
        }
        public void SwapUp(LabelItem item)
        {
            var sourceIndex = this.Items.IndexOf(item);
            var targetIndex = sourceIndex - 1;
            if (sourceIndex >= 1 && targetIndex >= 0)
            {
                this.Items.Swap(sourceIndex, targetIndex);
            }
            this.SetFormatIndexes();
        }
        public void SwapDown(LabelItem item)
        {
            var sourceIndex = this.Items.IndexOf(item);
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
                if (this.Items[i].Index != i + 1)
                {
                    this.Items[i].Index = i + 1;
                }
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
        internal List<LabelItem> GetPendingScanItems()
        {
            return this.Items.Where(x => x.Mode == Modes.Scann && String.IsNullOrEmpty(x.Value)).ToList();
        }
        public bool IsComplete()
        {
            return this.GetPendingScanItems().Count == 0;
        }
        public bool EqualsCode(Label label)
        {
            return this.Code == label.Code;
        }
    }
}
