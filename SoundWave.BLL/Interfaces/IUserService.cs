using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface IUserService
	{
		Task Create(UserDTO user);
		Task Update(UserDTO user);
		Task Delete(int id);
		Task<UserDTO> GetById(int id);
		Task<UserDTO> GetByName(string name);
		Task<IEnumerable<UserDTO>> ToList();
	}
}
