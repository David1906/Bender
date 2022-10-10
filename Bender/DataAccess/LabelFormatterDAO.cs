using Bender.Helpers;
using Bender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.DataAccess
{
    internal class LabelFormatterDAO : WithBenderContext
    {
        private LabelDAO LabelDAO { get; set; }
        private MainConfigDAO MainConfigDAO { get; set; }
        public LabelFormatterDAO(BenderContext? context = null) : base(context)
        {
            this.LabelDAO = new LabelDAO(this.Context);
            this.MainConfigDAO = new MainConfigDAO();
        }

        internal LabelFormatter? FindBySupplier(string supplier)
        {
            var labelIn = this.LabelDAO.FindBySupplier(supplier);
            var labelOut = this.LabelDAO.Find(this.MainConfigDAO.LabelOutName);
            if (labelIn == null || labelOut == null)
            {
                return null;
            }
            return new LabelFormatter(labelIn, labelOut);
        }
        internal LabelFormatter? FindByMainConfig()
        {
            var labelIn = this.LabelDAO.Find(this.MainConfigDAO.LabelInName);
            var labelOut = this.LabelDAO.Find(this.MainConfigDAO.LabelOutName);
            if (labelIn == null || labelOut == null)
            {
                return null;
            }
            return new LabelFormatter(labelIn, labelOut);
        }
    }
}
