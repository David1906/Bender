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

namespace Bender.Views.Components
{
    /// <summary>
    /// Interaction logic for CodeItem.xaml
    /// </summary>
    public partial class CodeItem : UserControl
    {
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(CodeItem), new PropertyMetadata("Field:"));



        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(CodeItem), new PropertyMetadata(""));

        public string Mode
        {
            get { return (string)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(string), typeof(CodeItem), new PropertyMetadata(""));

        public List<string> Modes { get; set; } = new List<string>
        {
            "Decode",
            "Fixed",
            "Scann",
            "Disabled",
        };

        public string Terminator
        {
            get { return (string)GetValue(TerminatorProperty); }
            set { SetValue(TerminatorProperty, value); }
        }
        public static readonly DependencyProperty TerminatorProperty = DependencyProperty.Register("Terminator", typeof(string), typeof(CodeItem), new PropertyMetadata(""));

        public int Idx
        {
            get { return (int)GetValue(IdxdxProperty); }
            set { SetValue(IdxdxProperty, value); }
        }
        public static readonly DependencyProperty IdxdxProperty = DependencyProperty.Register("Idx", typeof(int), typeof(CodeItem), new PropertyMetadata(0));

        public List<string> Terminators { get; set; } = new List<string>
        {
            ",",
            ";",
            ":",
            "None",
        };

        public CodeItem()
        {
            InitializeComponent();
        }

        private void CmbMode_Selected(object sender, RoutedEventArgs e)
        {
            var value = (sender as ComboBox)?.SelectedItem as string;
            Mode = value ?? "";
        }
    }
}
