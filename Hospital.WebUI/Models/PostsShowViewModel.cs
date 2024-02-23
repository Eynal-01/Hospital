using HospitalProject.Entities.DbEntities;

namespace Hospital.WebUI.Models
{
    public class PostsShowViewModel
    {
        public int PostId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? PublishTime { get; set; }
        public int ViewCount { get; set; }
        public List<string>? Images { get; set; }

        public virtual Admin? Admin { get; set; }
    }
}
