using Bender.DataAccess;
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
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class ConfigView : UserControl
    {
        private MainConfigDAO MainConfigDAO { get; set; } = new MainConfigDAO();
        private LabelDAO LabelDAO { get; set; } = new LabelDAO();
        internal List<string> FormatsNames
        {
            get
            {
                return this.LabelDAO.FindAllNames();
            }
        }


        public ConfigView()
        {
            InitializeComponent();
            this.ApplyCurrentConfigs();
        }

        public void ApplyCurrentConfigs()
        {
            this.TxtFocusWindow.Text = this.MainConfigDAO.FocusProcessName;
            this.CmbLabelIn.ItemsSource = this.FormatsNames;
            this.CmbLabelIn.SelectedItem = this.MainConfigDAO.LabelInName;
            this.CmbLabelOut.ItemsSource = this.FormatsNames;
            this.CmbLabelOut.SelectedItem = this.MainConfigDAO.LabelOutName;
        }
        private void CmbLabelIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var labelName = (sender as ComboBox)?.SelectedItem as string;
            if (!String.IsNullOrEmpty(labelName))
            {
                this.MainConfigDAO.LabelInName = labelName;
            }
        }
        private void CmbLabelOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var labelName = (sender as ComboBox)?.SelectedItem as string;
            if (!String.IsNullOrEmpty(labelName))
            {
                this.MainConfigDAO.LabelOutName = labelName;
            }
        }
        private void TxtFocusWindow_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MainConfigDAO.FocusProcessName = (sender as TextBox)?.Text ?? "";
        }
    }
}
