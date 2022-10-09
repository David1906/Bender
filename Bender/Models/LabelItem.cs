using Bender.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Bender.Models
{
    public class LabelItem
    {
        public int Index { get; set; }
        public string Value { get; set; } = "";
        public string Title { get; set; } = "";
        public string Key { get; set; } = "";
        public Modes Mode { get; set; } = Modes.Disabled;
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
    }
}
