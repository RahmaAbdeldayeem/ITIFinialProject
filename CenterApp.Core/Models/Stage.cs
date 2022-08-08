using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Stage
{

    public int Stage_Id { get; set; }
    public string Stage_Name { get; set; }
    public int Level_Id { get; set; }
    public virtual Level? Level { get; set; }
    public virtual IList<TeacherMatrial>? TeacherMatrial { get; set; }
    public virtual ICollection<Student>? Students { get; set; }


}