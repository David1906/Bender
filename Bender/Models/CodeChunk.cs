using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.Models
{
    public class CodeChunk
    {
        public int Index { get; set; }
        public string Value { get; set; } = "";
        public string Label { get; set; } = "";
        public string Property { get; set; } = "";
        public string Mode { get; set; } = "";
        public string Terminator { get; set; } = "";
        public bool IsDisabled
        {
            get
            {
                return Mode.Equals("Disabled", StringComparison.CurrentCultureIgnoreCase);
            }
        }
        public bool HasTerminator
        {
            get
            {
                return !Terminator.Equals("none", StringComparison.CurrentCultureIgnoreCase);
            }
        }
    }
}
