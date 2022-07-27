using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Group
{

    public int Group_Id { get; set; }
    public string Group_Name { get; set; }
    public int Teacher_Id { get; set; }
    public virtual Teacher Teacher { get; set; }
    public ICollection<StudentGroup> StudentGroup { get; set; }


}