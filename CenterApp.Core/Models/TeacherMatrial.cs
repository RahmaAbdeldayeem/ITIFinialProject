namespace CenterApp.Core.Models;

public class TeacherMatrial
{
    public int Matrial_Id { get; set; }
    public int Teacher_Id { get; set; }
    public int Stage_Id { get; set; }
    public virtual Stage Stage { get; set; }
    public virtual Teacher Teacher { get; set; }
    public virtual Matrial Matrial { get; set; }
}