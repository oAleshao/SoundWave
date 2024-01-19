using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SoundWave.DAL.Context;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Repositories
{
	public class HistoryRepository : IUsersHistory<History>
	{
		private SoundWaveContext db;

		public HistoryRepository(SoundWaveContext context)
		{
			this.db = context;
		}

		public async Task Create(History item)
		{
			await db.AddAsync(item);
		}

		/// <summary>
		/// Parameter id is the id of the user who submitted the request.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public async Task Delete(int id)
		{
			var histories = await db.histories.Where(h=>h.user.Id == id).ToListAsync();
			if (histories.Count != 0)
				foreach(var hSong in histories)
				{
					 db.histories.Remove(hSong);
				}
		}

		public async Task<IEnumerable<History>> ToList()
		{
			return await db.histories.ToListAsync();
		}
	}
}
