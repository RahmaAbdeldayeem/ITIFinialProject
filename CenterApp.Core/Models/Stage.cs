using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Stage
{

    public int Stage_Id { get; set; }
    public string Stage_Name { get; set; }
    public int Level_Id { get; set; }
    public virtual Level Level { get; set; }
    public IList<TeacherMatrial> TeacherMatrial { get; set; }
    public ICollection<Student> Students { get; set; }


}