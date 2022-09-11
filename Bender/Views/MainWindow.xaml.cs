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

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Models.Label Label { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var LabelItems = new List<Models.LabelItem>
            {
                new Models.LabelItem() { PropertyName = "Supplier", Terminator = Terminators.Comma, Title = "Supplier:", Mode = Modes.Fixed},
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
            this.Label = new Models.Label(LabelItems);
            this.RefreshListViewCodeItem();
            TxtCode.Focus();
        }

        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            this.Decode();
        }

        private void Decode()
        {
            this.Label.Code = TxtCode.Text;
            this.Label.Decode();
            this.RefreshListViewCodeItem();
            this.SetQrImage();
            TxtCode.Text = "";
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewCodeItem.SelectedItem as Models.LabelItem;
            if (selectedItem != null)
            {
                this.Label.SwapUp(selectedItem);
            }
            this.RefreshListViewCodeItem();
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewCodeItem.SelectedItem as Models.LabelItem;
            if (selectedItem != null)
            {
                this.Label.SwapDown(selectedItem);
            }
            this.RefreshListViewCodeItem();
        }
        private void RefreshListViewCodeItem()
        {
            ListViewCodeItem.ItemsSource = this.Label.Items;
            ListViewCodeItem.Items.Refresh();
        }


        public void SetQrImage()
        {
            ImgQr.Source = this.Label.GenerateQRCode();
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
    }
}
