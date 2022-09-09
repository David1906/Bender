using Bender.Models;
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

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<CodeChunk> Format { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Format = new ObservableCollection<CodeChunk>
            {
                new CodeChunk() { Property = "Supplier", Terminator = ",", Label = "Supplier:"},
                new CodeChunk() { Property = "Model", Terminator = ",", Label = "Model:"},
                new CodeChunk() { Property = "Rev", Terminator = ",", Label = "Rev:"},
                new CodeChunk() { Property = "SupplierPn", Terminator = ",", Label = "Supplier PN:"},
                new CodeChunk() { Property = "Qty", Terminator = ",", Label = "Qty:"},
                new CodeChunk() { Property = "HhPn", Terminator = ",", Label = "HH PN:"},
                new CodeChunk() { Property = "DateCode", Terminator = ",", Label = "Date Code:"},
                new CodeChunk() { Property = "LotNo", Terminator = ",", Label = "Lot No:"},
                new CodeChunk() { Property = "PkgId", Terminator = ",", Label = "Pkg Id:"},
                new CodeChunk() { Property = "WorkOrder", Terminator = "\n", Label = "Work Order:"},
            };
            lvDataBinding.ItemsSource = this.Format;
        }

        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            var sortedItems = this.Format.OrderBy(x => x.Label).ToList();
            this.Format.Clear();
            foreach (var item in sortedItems)
            {
                this.Format.Add(item);
            }
        }
    }
}
