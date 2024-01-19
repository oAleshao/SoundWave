using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface IUserLikesService
	{
		Task Create(UsersLikesDTO usersLikesDTO);
		Task Update(UsersLikesDTO usersLikesDTO, string action, bool active);
		Task<IEnumerable<UsersLikesDTO>> ToList(int id);
	}
}
