using Bender.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.Helpers
{
    public class WithBenderContext
    {
        internal BenderContext? _context;
        internal BenderContext Context
        {
            get
            {
                if (_context == null)
                {
                    _context = new BenderContext();
                }
                return _context;
            }
            set { _context = value; }
        }

        public WithBenderContext(BenderContext? context)
        {
            this._context = context;
        }
    }
}
