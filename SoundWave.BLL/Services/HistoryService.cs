using AutoMapper;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Services
{
	public class HistoryService : IHistoryService
	{
		IUnitOfWorks Database { get; set; }

		public HistoryService(IUnitOfWorks unit)
		{
			Database = unit;
		}


		public async Task Create(HistoryDTO item)
		{
			var newHistory = new History()
			{
				user = await Database.users.GetById(item.user.Id),
				song = await Database.songs.GetById(item.song.Id),
				date = item.date,
			};
			await Database.history.Create(newHistory);
			await Database.Save();
		}

		public async Task Delete(int id)
		{
			await Database.history.Delete(id);
		}

		public async Task<IEnumerable<HistoryDTO>> ToList()
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<History, HistoryDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<History>, IEnumerable<HistoryDTO>>(await Database.history.ToList());
		}
	}
}
