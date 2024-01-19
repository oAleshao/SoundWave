using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface IHistoryService
	{
		Task Create(HistoryDTO item);
		Task Delete(int id);
		Task<IEnumerable<HistoryDTO>> ToList();
	}
}
