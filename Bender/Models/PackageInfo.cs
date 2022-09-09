using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.Models
{
    public class PackageInfo
    {
        public string Supplier {get; set; } = "";
        public string Model {get; set; } = "";
        public string Rev {get; set; } = "";
        public string SupplierPn {get; set; } = "";
        public string Qty {get; set; } = "";
        public string HhPn {get; set; } = "";
        public string DateCode {get; set; } = "";
        public string LotNo {get; set; } = "";
        public string PkgId {get; set; } = "";
        public string WorkOrder { get; set; } = "";

        public void SetPropByName(string prop, object value)
        {
            this.GetType()?.GetProperty(prop)?.SetValue(this, value, null);
        }
    }
}
