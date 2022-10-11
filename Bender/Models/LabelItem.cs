using Bender.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;

namespace Bender.Models
{
    public class LabelItem : INotifyPropertyChanged
    {
        public int Index { get; set; }
        public string Value { get; set; } = "";
        public string Title { get; set; } = "";
        public string Key { get; set; } = "";
        private Modes _mode = Modes.Decode;
        public Modes Mode
        {
            get { return _mode; }
            set
            {
                if (value != _mode)
                {
                    _mode = value;
                    NotifyPropertyChanged("Mode");
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public Terminators Terminator { get; set; } = Terminators.Comma;
        public bool IsDisabled
        {
            get
            {
                return Mode.Equals(Modes.Disabled);
            }
        }
        public bool HasTerminator
        {
            get
            {
                return !Terminator.Equals(Terminators.None);
            }
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public string GetInstructionText()
        {
            var text = "";
            switch (this.Mode)
            {
                case Modes.Scann:
                    text = $"Scann {this.Title.Replace(":", "")}";
                    break;
            }
            return text;
        }
        public LabelItem Clone()
        {
            var labelItem = JsonSerializer.Deserialize<LabelItem>(
                JsonSerializer.Serialize(this));
            return labelItem ?? new LabelItem();
        }
    }
}
