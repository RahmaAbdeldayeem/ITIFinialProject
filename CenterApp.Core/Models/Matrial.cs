using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Matrial
{

    public int Matrial_Id { get; set; }
    public string Matrial_Name { get; set; }
    public IList<LevelMatrial> LevelMatrial { get; set; }
    public IList<TeacherMatrial> TeacherMatrial { get; set; }

    // public ICollection<Teacher> Teachers { get; set; }

}