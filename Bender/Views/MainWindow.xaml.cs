using System;
using System.Windows;
using System.ComponentModel;
using Bender.Models;
using Bender.Enums;
using Bender.DataAccess;
using Bender.Extensions;

namespace Bender.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LabelFormatterDAO LabelFormatterDAO { get; set; } = new LabelFormatterDAO();
        private MainConfigDAO MainConfigDAO { get; set; } = new MainConfigDAO();
        public static readonly DependencyProperty IsMenuVisibleProperty = DependencyProperty.Register("IsMenuVisible", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));
        public bool IsMenuVisible
        {
            get { return (bool)GetValue(IsMenuVisibleProperty); }
            set { SetValue(IsMenuVisibleProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            PositionWindowToRigthCenter();
        }

        private void ApplyLabelFormat()
        {
            var labelFormatter = this.LabelFormatterDAO.FindByMainConfig();
            if (labelFormatter == null)
            {
                this.ShowError(Message.InvalidFormat.ToDescriptionString());
                return;
            }
            this.LabelFormatterView.LabelFormatter = labelFormatter;
            this.LabelFormatterView.FocusMainTextField();
            this.LabelFormatterView.IsSelectFormatBySupplier = this.MainConfigDAO.SelectFormatBySupplier;
        }
        private void ShowError(string msg)
        {
            MessageBox.Show(msg);
        }
        private void PositionWindowToRigthCenter()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = (desktopWorkingArea.Bottom - this.Height) / 2;
        }
        private void root_Activated(object sender, EventArgs e)
        {
            this.LabelFormatterView.FocusMainTextField();
        }
        private void TabMain_Selected(object sender, RoutedEventArgs e)
        {
            this.ApplyLabelFormat();
        }
        private void TabFormats_Selected(object sender, RoutedEventArgs e)
        {

        }
        private void TabConfig_Selected(object sender, RoutedEventArgs e)
        {
            this.ConfigView.ApplyCurrentConfigs();
        }
    }
}
