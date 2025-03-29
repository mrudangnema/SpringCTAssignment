using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class StudentCourseRel
    {
        public int ID { get; set; }

        //ForeignKey["Student"]
        public int StudentID { get; set; }
        public required Student Student { get; set; }

        //ForeignKeyAttribute["Course"]
        public int CourseID { get; set; }
        public required Course Course { get; set; }
        public bool IsInactive { get; set; } = false;
        public DateTime AddedOn { get; set; }
    }
}
