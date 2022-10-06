using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using Bender.Models;
using Bender.Enums;

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for LabelView.xaml
    /// </summary>
    public partial class LabelView : UserControl
    {
        public Action<LabelItem>? ItemChanged;
        public Action<LabelItem?>? GotItemFocus;
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
        public static readonly DependencyProperty MyLabelProperty = DependencyProperty.Register("MyLabel", typeof(Models.Label), typeof(LabelView), new PropertyMetadata(null));


        public LabelView()
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

        public void SelectItem(LabelItem? labelItem)
        {
            this.LvLabelItems.SelectedItem = labelItem;
            if (labelItem != null)
            {
                FocusItem(labelItem);
            }
        }

        private void FocusItem(LabelItem labelItem)
        {
            this.LvLabelItems.UpdateLayout();
            var selectedItem = (ListBoxItem)this.LvLabelItems.ItemContainerGenerator.ContainerFromItem(labelItem);
            var txtValue = GetDescendantByType(selectedItem, typeof(TextBox), "TxtValue") as TextBox;
            if (txtValue != null)
            {
                txtValue.Focus();
            }
        }
        private Visual GetDescendantByType(Visual element, Type type, string name)
        {
            if (element == null) return null;
            if (element.GetType() == type)
            {
                FrameworkElement fe = element as FrameworkElement;
                if (fe != null)
                {
                    if (fe.Name == name)
                    {
                        return fe;
                    }
                }
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0;
                i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type, name);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }
        private void TxtValue_KeyUp(object sender, KeyEventArgs e)
        {
            var txtValue = sender as TextBox;
            if (e.Key == Key.Enter && this.ItemChanged != null)
            {
                this.ItemChanged((LabelItem)this.LvLabelItems.SelectedItem);
            }
            ((LabelItem)txtValue.DataContext).Value = txtValue?.Text ?? "";
        }
        private void TxtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            var txtValue = sender as TextBox;
            if (txtValue?.Text?.Contains("\n") == true && this.ItemChanged != null)
            {
                this.ItemChanged((LabelItem)this.LvLabelItems.SelectedItem);
            }
            ((LabelItem)txtValue.DataContext).Value = txtValue?.Text ?? "";
        }

        private void TxtValue_GotFocus(object sender, RoutedEventArgs e)
        {
            var txtValue = sender as TextBox;
            if (txtValue != null && this.GotItemFocus != null)
            {
                this.GotItemFocus((LabelItem)txtValue.DataContext);

            }
        }
        private void Chip_Click(object sender, RoutedEventArgs e)
        {
            var chip = sender as MaterialDesignThemes.Wpf.Chip;
            if (chip != null)
            {
                this.SelectItem((LabelItem)chip.DataContext);
            }
        }
    }
}
