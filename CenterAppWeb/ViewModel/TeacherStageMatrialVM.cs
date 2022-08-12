using CenterApp.Core.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CenterAppWeb.ViewModel
{
    public class TeacherStageMatrialVM
    {
        public Teacher Teacher { get; set; }
        public int? Level_Id { get; set; }
        public int? Matrial_Id { get; set; }
        public int? Stage_Id { get; set; }

        public IFormFile? file { get; set; }

    }
}
