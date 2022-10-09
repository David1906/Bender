using Bender.Enums;
using Bender.Helpers;
using Bender.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Bender.DataAccess
{
    [Table("LabelItems")]
    public class LabelItemDAO
    {
        [Key]
        public int LabelItemId { get; set; }
        [Required]
        public LabelDAO? Label { get; set; }
        public int Index { get; set; }
        public string Title { get; set; } = "";
        public string Key { get; set; } = "";
        public Modes Mode { get; set; } = Modes.Disabled;
        public Terminators Terminator { get; set; } = Terminators.Comma;
    }
}
