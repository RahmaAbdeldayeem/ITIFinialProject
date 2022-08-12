using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel;

public class LevelMatrialStagesVM
{
    public Teacher Teacher { get; set; }
    public int Level_Id { get; set; }
    public List<Stage> Stages { get; set; }
   
    public List<LevelMatrial> LevelMatrials{ get; set; }
}