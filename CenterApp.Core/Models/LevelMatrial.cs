using System.ComponentModel.DataAnnotations;

namespace CenterApp.Core.Models;

public class LevelMatrial
{
    public int Level_Id { get; set; }
    public int Matrial_Id { get; set; }
    public Matrial Matrial { get; set; }
    public Level Level { get; set; }
}