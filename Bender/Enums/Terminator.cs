using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.Enums
{
    public enum Terminators
    {
        [Description("Comma [ , ]")]
        [DefaultValue(",")]
        Comma,
        [Description("SemiColon [ ; ]")]
        [DefaultValue(";")]
        SemiColon,
        [Description("Colon [ : ]")]
        [DefaultValue(":")]
        Colon,
        [Description("CR")]
        [DefaultValue("\n")]
        CR,
        [Description("None")]
        [DefaultValue("")]
        None
    }
}
