using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterApp.Core.Models
{
    public class Student
    {
        public int Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Student_Address { get; set; }
        public DateTime Student_BirthOfDate { get; set; }
        public bool Gender { get; set; }
        public DateTime Student_RegisterDate { get; set; }
        public string Student_Email { get; set; }
        public string? Student_Image { get; set; }
        public string Student_StdPhone { get; set; }
        public string Student_ParentPhone { get; set; }
        public int Stage_id { get; set; }
        public virtual Stage? Stage { get; set; }
        public ICollection<StudentGroup>? StudentGroup { get; set; }
        public ICollection<StudentPayments>? StudentPayments { get; set; }

    }
}
