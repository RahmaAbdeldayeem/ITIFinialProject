using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Level
{

    public int Level_Id { get; set; }
    public string Level_Name { get; set; }
    public virtual ICollection<Stage>? Stages { get; set; }
    public virtual IList<LevelMatrial>? LevelMatrial { get; set; }


}