﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            ApplyLabelFormat();
            PositionWindowToRigthCenter();
        }

        private void ApplyLabelFormat()
        {
            var mainConfig = new MainConfigDAO();
            var labelDAO = new LabelDAO();
            var labelIn = labelDAO.Find(mainConfig.LabelInName);
            var labelOut = labelDAO.Find(mainConfig.LabelOutName);
            if (labelIn == null || labelOut == null)
            {
                this.ShowError(Message.InvalidFormat.ToDescriptionString());
                return;
            }
            this.LabelFormatterView.LabelFormatter = new LabelFormatter(labelIn, labelOut);
            this.LabelFormatterView.FocusTxtCode();
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
            this.LabelFormatterView.FocusTxtCode();
        }
    }
}
