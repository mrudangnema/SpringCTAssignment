using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Student
    {
        public int ID { get; set; }
        [MaxLength(20)]
        public string StudentID { get; set; } = string.Empty;

        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;
        public DateTime AddedOn { get; set; }
        public bool IsInactive { get; set; } = false;
    }
}
