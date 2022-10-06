using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System;

namespace Bender.Models
{
    public class LabelFormatter
    {
        public Label LabelIn { get; set; }
        public Label LabelOut { get; set; }

        public LabelFormatter(Label labelIn, Label labelOut)
        {
            this.LabelIn = labelIn;
            this.LabelOut = labelOut;
        }

        public bool CanGenerateQr()
        {
            return !this.LabelIn.EqualsCode(this.LabelOut) && this.LabelIn.IsComplete();
        }
        public void Decode(string code)
        {
            this.LabelIn.Code = code;
            this.LabelIn.Decode();
        }
        public BitmapImage GenerateQrImage()
        {
            Contract.Assert(this.CanGenerateQr());
            this.LabelOut.CopyValues(this.LabelIn);
            return this.LabelOut.GenerateQRCode();
        }
        internal LabelItem? GetNextPendingScanItem()
        {
            return this.LabelIn.GetPendingScanItems().FirstOrDefault();
        }
    }
}
