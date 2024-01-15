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
	public class SongRepository : IRepository<Song>
	{
		private SoundWaveContext db;

		public SongRepository(SoundWaveContext context)
		{
			this.db = context;
		}

		public async Task Create(Song item)
		{
			await db.songs.AddAsync(item);
		}

		public async Task Delete(int id)
		{
			var song = await db.songs.FindAsync(id);
			if (song != null)
				db.songs.Remove(song);
		}

		public async Task<Song> GetById(int id)
		{
			return await db.songs.FindAsync(id);
		}

		/// <summary>
		/// param name is user's login
		/// </summary>
		/// <param name="name"></param>
		/// <returns> The entity found, or null. </returns>
		public async Task<Song> GetByName(string name)
		{
			var song = await db.songs.Where(s => s.Title == name).FirstOrDefaultAsync();
			return song;
		}

		public async Task<IEnumerable<Song>> ToList()
		{
			return await db.songs.ToListAsync();
		}

		public void Update(Song item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}
