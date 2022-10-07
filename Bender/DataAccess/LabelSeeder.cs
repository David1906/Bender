using Bender.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.DataAccess
{
    public class LabelSeeder
    {
        private BenderContext Context { get; set; }

        public LabelSeeder(BenderContext context)
        {
            this.Context = context;
        }

        public void Seed()
        {
            var labelInItems = new List<Models.LabelItem>
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
            new LabelDAO(this.Context).Add("AVANTE", labelInItems);

            var labelOutItems = new List<Models.LabelItem>
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
            new LabelDAO(this.Context).Add("AUTO_LABEL_OUT", labelOutItems);
        }
    }
}
