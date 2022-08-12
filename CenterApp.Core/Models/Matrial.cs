using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Matrial
{

    public int Matrial_Id { get; set; }
    public string Matrial_Name { get; set; }
    public virtual IList<LevelMatrial>? LevelMatrial { get; set; }
    public virtual IList<TeacherMatrial>? TeacherMatrial { get; set; }
    public ICollection<StudentPayments> StudentPayments { get; set; }

    // public ICollection<Teacher> Teachers { get; set; }

}