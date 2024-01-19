using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface IPlaylistService
	{
		Task Create(PlaylistDTO playlist);
		Task Update(PlaylistDTO playlist);
		Task Delete(int id);
		Task<PlaylistDTO> GetById(int id);
		Task<PlaylistDTO> GetByName(string name);
		Task<IEnumerable<PlaylistDTO>> ToList();
	}
}
