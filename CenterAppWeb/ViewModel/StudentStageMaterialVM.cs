

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
        public IFormFile File { get; set; }
        
    }
}
