using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class StudentCreateVm
    {
        public int Level_Id { get; set; }
        public List<Level> Levels { get; set; }

        public List<StudentCreateVm> StudentCreates { get; set; }
    }
}
