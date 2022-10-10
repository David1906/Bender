using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bender.DataAccess
{
    public class MainConfigDAO
    {
        public string LabelInName
        {
            get
            {
                return Properties.Settings.Default.LABEL_IN_NAME;
            }
            set
            {
                Properties.Settings.Default.LABEL_IN_NAME = value;
                Properties.Settings.Default.Save();
            }
        }
        public string LabelOutName
        {
            get
            {
                return Properties.Settings.Default.LABEL_OUT_NAME;
            }
            set
            {
                Properties.Settings.Default.LABEL_OUT_NAME = value;
                Properties.Settings.Default.Save();
            }
        }
        public string FocusProcessName
        {
            get
            {
                return Properties.Settings.Default.FOCUS_PROCESS_NAME;
            }
            set
            {
                Properties.Settings.Default.FOCUS_PROCESS_NAME = value;
                Properties.Settings.Default.Save();
            }
        }
        public bool SelectFormatBySupplier
        {
            get
            {
                return Properties.Settings.Default.SELECT_FORMAT_BY_SUPPLIER;
            }
            set
            {
                Properties.Settings.Default.SELECT_FORMAT_BY_SUPPLIER = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
