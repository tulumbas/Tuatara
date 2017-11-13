namespace Tuatara.Services.Dto
{
    public class ProjectDtoWithParent: ProjectDto
    {
        public string ParentName { get; set; }
    }

    public class ProjectDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
    }
}