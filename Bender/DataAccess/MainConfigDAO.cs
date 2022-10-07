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
    }
}
