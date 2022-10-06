using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Bender.Enums
{
    public enum Modes
    {
        [Description("Decode")]
        Decode,
        [Description("Fixed")]
        Fixed,
        [Description("Scann")]
        Scann,
        [Description("Disabled")]
        Disabled
    }
}
