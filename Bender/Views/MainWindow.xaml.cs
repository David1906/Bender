using Bender.Models;
using Bender.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bender.Extensions;
using System.Reflection;
using System.IO;
using QRCoder;
using System.Drawing;
using Bender.Enums;
using System.ComponentModel;

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.Label LabelIn { get; set; }
        private Models.Label LabelOut { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var LabelInItems = new BindingList<Models.LabelItem>
            {
                new Models.LabelItem() { PropertyName = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "PkgId", Terminator = Terminators.None, Title = "Pkg Id:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Supplier", Terminator = Terminators.None, Title = "Supplier:", Mode = Modes.Fixed},

                new Models.LabelItem() { PropertyName = "Model", Terminator = Terminators.None, Title = "Model:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "Rev", Terminator = Terminators.None, Title = "Rev:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "WorkOrder", Terminator = Terminators.None, Title = "Work Order:", Mode = Modes.Disabled},
            };
            this.LabelIn = new Models.Label(LabelInItems);
            LabelInComponent.MyLabel = this.LabelIn;

            var LabelOutItems = new BindingList<Models.LabelItem>
            {
                new Models.LabelItem() { PropertyName = "Supplier", Terminator = Terminators.Comma, Title = "Supplier:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "PkgId", Terminator = Terminators.None, Title = "Pkg Id:", Mode = Modes.Decode},

                new Models.LabelItem() { PropertyName = "Model", Terminator = Terminators.None, Title = "Model:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "Rev", Terminator = Terminators.None, Title = "Rev:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "WorkOrder", Terminator = Terminators.None, Title = "Work Order:", Mode = Modes.Disabled},
            };
            this.LabelOut = new Models.Label(LabelOutItems);
            LabelOutComponent.MyLabel = this.LabelOut;

            TxtCode.Focus();
        }

        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            this.Decode();
        }

        private void Decode()
        {
            this.LabelIn.Code = TxtCode.Text;
            this.LabelIn.Decode();
            this.SetQrImage();
            TxtCode.Text = "";
        }

        public void SetQrImage()
        {
            this.LabelOut.CopyValues(this.LabelIn);
            ImgQr.Source = this.LabelOut.GenerateQRCode();
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
            this.SetQrImage();
        }
    }
}
