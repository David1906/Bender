using Bender.Enums;
using Bender.Extensions;
using Bender.Helpers;
using Bender.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;
using System;
using Bender.DataAccess;

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for LabelFormatterView.xaml
    /// </summary>
    public partial class LabelFormatterView : UserControl
    {
        private MainConfigDAO MainConfigDAO { get; set; } = new MainConfigDAO();
        private LabelFormatter? _labelFormatter;
        public LabelFormatter? LabelFormatter
        {
            get
            {
                return _labelFormatter;
            }
            set
            {
                _labelFormatter = value;
                if (_labelFormatter != null)
                {
                    LabelInComponent.MyLabel = _labelFormatter.LabelIn;
                }
            }
        }

        public LabelFormatterView()
        {
            InitializeComponent();
            LabelInComponent.GotItemFocus += this.OnGotItemFocus;
            LabelInComponent.ItemChanged += this.OnLabelItemChange;
        }

        private void OnGotItemFocus(LabelItem? labelItem)
        {
            var text = "";
            if (labelItem != null)
            {
                text = labelItem.GetInstructionText();
            }
            TxtMsg.Text = text;
        }
        private void OnLabelItemChange(LabelItem labelItem)
        {
            this.Format();
        }
        private void TxtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                this.Decode();
            }
        }
        private void TxtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtCode.Text.Contains("\n"))
            {
                this.Decode();
            }
        }
        private void BtnReload_Click(object sender, RoutedEventArgs e)
        {
            this.Format();
        }
        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            this.Decode();
        }
        private void TxtCode_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtMsg.Text = Message.ScannPackageCode.ToDescriptionString();
        }
        private void Decode()
        {
            this.LabelFormatter?.Decode(TxtCode.Text);
            this.Format();
            TxtCode.Clear();
        }
        public void Format()
        {
            BitmapImage? bitmapImage = null;
            LabelItem? labelItem = null;
            if (this.LabelFormatter?.CanGenerateQr() == true)
            {
                bitmapImage = this.LabelFormatter.GenerateQrImage();
                ProcessHelper.SetFocusToExternalApp(this.MainConfigDAO.FocusProcessName);
                TxtCode.Focus();
            }
            else
            {
                labelItem = this.LabelFormatter?.GetNextPendingScanItem();
            }
            ImgQr.Source = bitmapImage;
            LabelInComponent.SelectItem(labelItem);
        }
        public void FocusTxtCode()
        {
            TxtCode.Focus();
            TxtCode.SelectAll();
        }
    }
}
