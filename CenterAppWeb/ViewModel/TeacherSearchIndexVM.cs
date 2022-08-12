using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class TeacherSearchIndexVM
    {
        public ICollection<Teacher> Teachers { get; set; }
        public int? SearchByID { get; set; }
        public string SearchByName { get; set; }
        public string SearchByPhone { get; set; }
        public int? SearchBySubject { get; set; }


    }
}
