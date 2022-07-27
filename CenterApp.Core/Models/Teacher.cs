using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Teacher
{

    public int Teacher_Id { get; set; }
    public string Teacher_Name { get; set; }
    public string Teacher_Phone { get; set; }
    public string Teacher_Email { get; set; }
    public string Teacher_Specilist { get; set; }
    public string Teacher_Image { get; set; }
    public DateTime Teacher_BirthOfDate { get; set; }
    public ICollection<Group> Groups { get; set; }
    public IList<TeacherMatrial> TeacherMatrial { get; set; }

}