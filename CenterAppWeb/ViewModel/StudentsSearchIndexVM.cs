using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class StudentsSearchIndexVM
    {
        public ICollection<Student> Students { get; set; }

        public string SearchByName { get; set; }    

        public int SearchById { get; set; }

        public string Student_Image { get; set; }
    }
}
