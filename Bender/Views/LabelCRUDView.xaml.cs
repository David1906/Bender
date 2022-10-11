using Bender.DataAccess;
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

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for LabelCRUDView.xaml
    /// </summary>
    public partial class LabelCRUDView : UserControl
    {
        private LabelDAO LabelDAO { get; set; } = new LabelDAO();
        private Models.Label? _selectedLabel;
        private Models.Label SelectedLabel
        {
            get { return _selectedLabel; }
            set
            {
                _selectedLabel = value;
                this.ApplySelectedLabel();
            }
        }
        private Models.Label BeforeUpdateLabel { get; set; }
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(StatesCRUD), typeof(LabelCRUDView), new PropertyMetadata(StatesCRUD.Disabled));
        public StatesCRUD State
        {
            get { return (StatesCRUD)GetValue(StateProperty); }
            set
            {
                SetValue(StateProperty, value);
                this.ApplyState();
            }
        }

        public LabelCRUDView()
        {
            InitializeComponent();
            this.SelectedLabel = new Models.Label();
            this.BeforeUpdateLabel = this.SelectedLabel;
            this.ApplyState();
            this.RefreshCmbLabels();
        }
        private void RefreshCmbLabels()
        {
            var labels = this.LabelDAO.FindAll();
            this.CmbLabels.ItemsSource = labels;
            var labelIndex = labels.FindIndex(x => x.LabelId == this.SelectedLabel.LabelId);
            if (labelIndex < 0)
            {
                labelIndex = labels.Count - 1;
            }
            this.CmbLabels.SelectedIndex = labelIndex;
            this.State = StatesCRUD.Read;
        }
        private void ApplyState()
        {
            if (this.State == StatesCRUD.Disabled)
            {
                BtnCreate.IsEnabled = true;
                BtnUpdate.IsEnabled = false;
                BtnDelete.IsEnabled = false;
                BtnCancel.Visibility = Visibility.Hidden;
                BtnSave.Visibility = Visibility.Hidden;
            }
            else if (this.State == StatesCRUD.Read)
            {
                BtnCreate.IsEnabled = true;
                BtnUpdate.IsEnabled = true;
                BtnDelete.IsEnabled = true;
                BtnCancel.Visibility = Visibility.Hidden;
                BtnSave.Visibility = Visibility.Hidden;
            }
            else
            {
                BtnCreate.IsEnabled = this.State == StatesCRUD.Create;
                BtnUpdate.IsEnabled = this.State == StatesCRUD.Update;
                BtnDelete.IsEnabled = this.State == StatesCRUD.Delete;
                BtnCancel.Visibility = Visibility.Visible;
                BtnSave.Visibility = Visibility.Visible;
            }
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedLabel = new Models.Label();
            this.State = StatesCRUD.Create;
            this.CmbLabels.SelectedIndex = -1;
            TxtName.Focus();
            TxtName.SelectAll();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            this.BeforeUpdateLabel = this.SelectedLabel;
            this.SelectedLabel = this.SelectedLabel.Clone();
            this.State = StatesCRUD.Update;
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to permanently delete the format \"{this.SelectedLabel.Name}\"?", "Delete Format", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.LabelDAO.Remove(this.SelectedLabel);
                this.SelectedLabel = this.BeforeUpdateLabel;
                this.RefreshCmbLabels();
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedLabel = this.BeforeUpdateLabel;
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.LabelDAO.Save(this.SelectedLabel);
            this.RefreshCmbLabels();
        }
        private void CmbLabels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var label = (sender as ComboBox)?.SelectedItem as Models.Label;
            if (label != null)
            {
                this.SelectedLabel = label;
            }
        }
        private void ApplySelectedLabel()
        {
            TxtName.Text = this.SelectedLabel.Name;
            TxtSupplier.Text = this.SelectedLabel.Supplier;
            this.LabelComponent.MyLabel = this.SelectedLabel;
            this.State = StatesCRUD.Read;
        }
        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SelectedLabel.Name = TxtName.Text;
        }
        private void TxtSupplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SelectedLabel.Supplier = TxtSupplier.Text;
        }
    }
}
