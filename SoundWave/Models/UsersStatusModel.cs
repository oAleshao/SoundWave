
using SoundWave.BLL.DTO;

namespace SoundWave.Models
{
    public class UsersStatusModel
    {
        public int Id { get; set; }
        public UserDTO? User { get; set; }
        public ICollection<UserDTO>? users { get; set; }
    }
}
