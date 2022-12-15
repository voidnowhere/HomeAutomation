using HomeAutomation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeAutomation
{
    [Index(nameof(Login), IsUnique = true)]
    [Index(nameof(NIC), IsUnique = true)]
    internal class Person
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Login { get; set; }

        [MaxLength(100)]
        public string Type { get; set; }

        [MaxLength(100)]
        [DefaultValue("123456")]
        public string Password { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(8)]
        public string NIC { get; set; }

        public DateOnly BirthDay { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [DefaultValue(true)]
        public bool IsAcive { get; set; }

        public List<Log> Logs { get; set; }
    }
}
