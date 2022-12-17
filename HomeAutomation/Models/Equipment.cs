using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models
{
    [Index(nameof(Name), IsUnique = true)]
    internal class Equipment
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public bool Up { get; set; }

        public virtual Room Room { get; set; }

        public virtual List<Log> Logs { get; set; }
    }
}
