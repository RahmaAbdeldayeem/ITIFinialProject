namespace CenterApp.Core.Models;

public class StudentGroup
{
    public int Group_Id { get; set; }
    public int Student_Id { get; set; }
    public virtual Group Group { get; set; }
    public virtual Student Student { get; set; }
}