﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation.Models
{
    internal class Log
    {
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime When { get; set; }

        [MaxLength(100)]
        public string Status { get; set; }

        public Person Person { get; set; }

        public Equipment Equipment { get; set; }
    }
}