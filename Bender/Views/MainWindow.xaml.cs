using System;
using System.Windows;
using System.ComponentModel;
using Bender.Models;
using Bender.Enums;

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
            var LabelInItems = new BindingList<Models.LabelItem>
            {
                new Models.LabelItem() { PropertyName = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "PkgId", Terminator = Terminators.Comma, Title = "Pkg Id:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Supplier", Terminator = Terminators.None, Title = "Supplier:", Mode = Modes.Scann},

                new Models.LabelItem() { PropertyName = "Model", Terminator = Terminators.None, Title = "Model:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "Rev", Terminator = Terminators.None, Title = "Rev:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "WorkOrder", Terminator = Terminators.None, Title = "Work Order:", Mode = Modes.Disabled},
            };
            var LabelOutItems = new BindingList<Models.LabelItem>
            {
                new Models.LabelItem() { PropertyName = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "PkgId", Terminator = Terminators.Comma, Title = "Pkg Id:", Mode = Modes.Decode},
                new Models.LabelItem() { PropertyName = "Supplier", Terminator = Terminators.None, Title = "Supplier:", Mode = Modes.Decode},

                new Models.LabelItem() { PropertyName = "Model", Terminator = Terminators.None, Title = "Model:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "Rev", Terminator = Terminators.None, Title = "Rev:", Mode = Modes.Disabled},
                new Models.LabelItem() { PropertyName = "WorkOrder", Terminator = Terminators.None, Title = "Work Order:", Mode = Modes.Disabled},
            };
            this.LabelFormatterView.LabelFormatter = new LabelFormatter(
                new Models.Label(LabelInItems),
                new Models.Label(LabelOutItems));
            this.LabelFormatterView.FocusTxtCode();
            PositionWindowToRigthCenter();
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
