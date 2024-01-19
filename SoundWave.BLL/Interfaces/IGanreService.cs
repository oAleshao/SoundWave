using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface IGanreService
	{
		Task Create(GanreDTO ganre);
		Task Update(GanreDTO ganre);
		Task Delete(int id);
		Task<GanreDTO> GetById(int id);
		Task<GanreDTO> GetByName(string name);
		Task<IEnumerable<GanreDTO>> ToList();

	}
}
