using Bender.Enums;
using System;
using System.Collections.Generic;
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

namespace Bender.Views.Components
{
    /// <summary>
    /// Interaction logic for LabelComponent.xaml
    /// </summary>
    public partial class LabelComponent : UserControl
    {

        public IEnumerable<Modes> ModesValues
        {
            get { return Enum.GetValues(typeof(Modes)).Cast<Modes>(); }
        }

        public IEnumerable<Terminators> TerminatorsValues
        {
            get { return Enum.GetValues(typeof(Terminators)).Cast<Terminators>(); }
        }

        public Models.Label MyLabel
        {
            get { return (Models.Label)GetValue(MyLabelProperty); }
            set { SetValue(MyLabelProperty, value); RefreshListViewCodeItem(); }
        }
        public static readonly DependencyProperty MyLabelProperty = DependencyProperty.Register("MyLabel", typeof(Models.Label), typeof(LabelComponent), new PropertyMetadata(null));


        public LabelComponent()
        {
            InitializeComponent();
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LvLabelItems.SelectedItem as Models.LabelItem;
            if (selectedItem != null)
            {
                this.MyLabel.SwapUp(selectedItem);
            }
            this.RefreshListViewCodeItem();
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = LvLabelItems.SelectedItem as Models.LabelItem;
            if (selectedItem != null)
            {
                this.MyLabel.SwapDown(selectedItem);
            }
            this.RefreshListViewCodeItem();
        }

        private void RefreshListViewCodeItem()
        {
            LvLabelItems.ItemsSource = this.MyLabel.Items;
            LvLabelItems.Items.Refresh();
        }
    }
}
