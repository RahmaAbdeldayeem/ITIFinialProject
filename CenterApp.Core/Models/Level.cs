using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class Level
{

    public int Level_Id { get; set; }
    public string Level_Name { get; set; }
    public ICollection<Stage> Stages { get; set; }
    public IList<LevelMatrial> LevelMatrial { get; set; }


}