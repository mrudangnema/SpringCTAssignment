using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Course
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public DateTime AddedOn { get; set; }
        public bool IsInactive { get; set; } = false;

    }
}
