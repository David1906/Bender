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

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Format Format { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            var FormatItems = new List<CodeChunk>
            {
                new CodeChunk() { Property = "Supplier", Terminator = ",", Label = "Supplier:", Mode = "Fixed"},
                new CodeChunk() { Property = "HhPn", Terminator = ",", Label = "HH PN:", Mode = "Decode"},
                new CodeChunk() { Property = "Qty", Terminator = ",", Label = "Qty:", Mode = "Decode"},
                new CodeChunk() { Property = "SupplierPn", Terminator = ",", Label = "Supplier PN:", Mode = "Decode"},
                new CodeChunk() { Property = "DateCode", Terminator = ",", Label = "Date Code:", Mode = "Decode"},
                new CodeChunk() { Property = "LotNo", Terminator = ",", Label = "Lot No:", Mode = "Decode"},
                new CodeChunk() { Property = "PkgId", Terminator = "None", Label = "Pkg Id:", Mode = "Decode"},

                new CodeChunk() { Property = "Model", Terminator = "None", Label = "Model:", Mode = "Disabled"},
                new CodeChunk() { Property = "Rev", Terminator = "None", Label = "Rev:", Mode = "Disabled"},
                new CodeChunk() { Property = "WorkOrder", Terminator = "None", Label = "Work Order:", Mode = "Disabled"},
            };
            this.Format = new Format(FormatItems);
            this.RefreshListViewCodeItem();
            TxtCode.Focus();
        }

        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            this.Decode();
        }

        private void Decode()
        {
            var code = new Code(TxtCode.Text);
            var packageInfo = code.Decode(this.Format);
            foreach (PropertyInfo prop in packageInfo.GetType().GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                if (type == typeof(string))
                {
                    var value = prop.GetValue(packageInfo, null)?.ToString() ?? "";
                    this.Format.SetProp(prop.Name, value);
                }
            }
            this.RefreshListViewCodeItem();
            this.SetQrImage(code);
            TxtCode.Text = "";
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewCodeItem.SelectedItem as CodeChunk;
            if (selectedItem != null)
            {
                this.Format.SwapUp(selectedItem);
            }
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListViewCodeItem.SelectedItem as CodeChunk;
            if (selectedItem != null)
            {
                this.Format.SwapDown(selectedItem);
            }
        }
        private void RefreshListViewCodeItem()
        {
            ListViewCodeItem.ItemsSource = this.Format.Items;
            ListViewCodeItem.Items.Refresh();
        }


        public void SetQrImage(Code code)
        {
            ImgQr.Source = code.GenerateQr(this.Format);
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
