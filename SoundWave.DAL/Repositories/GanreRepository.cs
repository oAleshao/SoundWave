using Microsoft.EntityFrameworkCore;
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
	public class GanreRepository : IRepository<Ganre>
	{
		private SoundWaveContext db;

		public GanreRepository(SoundWaveContext context)
		{
			this.db = context;
		}


		public async Task Create(Ganre item)
		{
			await db.ganres.AddAsync(item);
		}

		public async Task Delete(int id)
		{
			var ganre = await db.ganres.FindAsync(id);
			if (ganre != null)
				db.ganres.Remove(ganre);
		}

		public async Task<Ganre> GetById(int id)
		{
			var ganre = await db.ganres.FindAsync(id);
			return ganre;
		}

		/// <summary>
		/// param name is user's login
		/// </summary>
		/// <param name="name"></param>
		/// <returns> The entity found, or null. </returns>
		public async Task<Ganre> GetByName(string name)
		{
			var ganre = await db.ganres.Where(g => g.Title == name).FirstOrDefaultAsync();
			return ganre;
		}

		public async Task<IEnumerable<Ganre>> ToList()
		{
			return await db.ganres.ToListAsync();
		}

		public void Update(Ganre item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}
