using Core.Entitiess.Abstract;


namespace Entities.Dto
{
	public class UsersAdminDto : IDto
    {
        public int Id { get; set; }
        public string NameSurName { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Roles { get; set; }
    }
}
