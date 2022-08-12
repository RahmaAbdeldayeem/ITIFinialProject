

using CenterApp.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CenterAppWeb.ViewModel
{
    public class StudentStageMaterialVM
    {
        public Student Student { get; set; }
        public int? Level_Id { get; set; }
        public int? Matrial_Id { get; set; }

        public int? Stage_Id { get; set; }
        public SelectList ?StageList { get; set; }
        public SelectList ?LevelList { get; set; }
        public SelectList ?MatrialList { get; set; }
        
    }
}
