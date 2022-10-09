﻿using Bender.Enums;
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
                new Models.LabelItem() {Index = 0, Key = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 1, Key = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 2, Key = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 3, Key = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 4, Key = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 5, Key = "PkgId", Terminator = Terminators.Comma, Title = "Pkg Id:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 6, Key = "Supplier", Terminator = Terminators.None, Title = "Supplier:", Mode = Modes.Scann}
            };
            new LabelDAO(this.Context).Add("AVANTE", labelInItems);

            var labelOutItems = new List<Models.LabelItem>
            {
                new Models.LabelItem() {Index = 0, Key = "HhPn", Terminator = Terminators.Comma, Title = "HH PN:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 1, Key = "Qty", Terminator = Terminators.Comma, Title = "Qty:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 2, Key = "SupplierPn", Terminator = Terminators.Comma, Title = "Supplier PN:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 3, Key = "DateCode", Terminator = Terminators.Comma, Title = "Date Code:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 4, Key = "LotNo", Terminator = Terminators.Comma, Title = "Lot No:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 5, Key = "PkgId", Terminator = Terminators.Comma, Title = "Pkg Id:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 6, Key = "Supplier", Terminator = Terminators.None, Title = "Supplier:", Mode = Modes.Decode}
            };
            new LabelDAO(this.Context).Add("AUTO_LABEL_OUT", labelOutItems);

            var test = new List<Models.LabelItem>
            {
                new Models.LabelItem() {Index = 0, Key = "test", Terminator = Terminators.Comma, Title = "Test1:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 1, Key = "test", Terminator = Terminators.Comma, Title = "Test2:", Mode = Modes.Decode},
                new Models.LabelItem() {Index = 2, Key = "test", Terminator = Terminators.Comma, Title = "Test3:", Mode = Modes.Decode}
            };
            new LabelDAO(this.Context).Add("TEST", test);
        }
    }
}