namespace Tuatara.Data.Dto
{
    public class ProjectDto
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public int? ParentID { get; set; }
    }
}