using Bender.Models;
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
using Bender.Enums;

namespace Bender.Views.Components
{
    /// <summary>
    /// Interaction logic for CodeItem.xaml
    /// </summary>
    public partial class LabelItemControl : UserControl
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(LabelItemControl), new PropertyMetadata("Field:"));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(LabelItemControl), new PropertyMetadata(""));

        public IEnumerable<Modes> ModesValues
        {
            get { return Enum.GetValues(typeof(Modes)).Cast<Modes>(); }
        }
        public Modes Mode
        {
            get { return (Modes)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(Modes), typeof(LabelItemControl), new PropertyMetadata(Modes.Disabled));

        public IEnumerable<Terminators> TerminatorsValues
        {
            get { return Enum.GetValues(typeof(Terminators)).Cast<Terminators>(); }
        }
        public Terminators Terminator
        {
            get { return (Terminators)GetValue(TerminatorProperty); }
            set { SetValue(TerminatorProperty, value); }
        }
        public static readonly DependencyProperty TerminatorProperty = DependencyProperty.Register("Terminator", typeof(Terminators), typeof(LabelItemControl), new PropertyMetadata(Terminators.Comma));

        public int Idx
        {
            get { return (int)GetValue(IdxdxProperty); }
            set { SetValue(IdxdxProperty, value); }
        }
        public static readonly DependencyProperty IdxdxProperty = DependencyProperty.Register("Idx", typeof(int), typeof(LabelItemControl), new PropertyMetadata(0));

        public LabelItemControl()
        {
            InitializeComponent();
        }

        private void CmbMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = (ComboBox)sender;
            var opacity = 1.0;
            if ((Modes)cmb.SelectedItem == Modes.Disabled)
            {
                opacity = 0.3;
            }
            this.Opacity = opacity;
        }
    }
}
