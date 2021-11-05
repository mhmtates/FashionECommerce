using Core.Entitiess.Abstract;

namespace Entities.Dto
{
    public class UserApiLoginDto: IDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
    }
}
