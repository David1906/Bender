using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Bender.Enums
{
    public enum Message
    {
        [Description("Scann Package 2D Code")]
        ScannPackageCode,
        [Description("Invalid format, please verify configuration")]
        InvalidFormat,
        [Description("Scann Supplier")]
        ScannSupplier
    }
}
