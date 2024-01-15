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
	public class PlaylistRepository : IRepository<Playlist>
	{
		private SoundWaveContext db;

		public PlaylistRepository(SoundWaveContext context)
		{
			this.db = context;
		}

		public async Task Create(Playlist item)
		{
			await db.playlists.AddAsync(item);
		}

		public async Task Delete(int id)
		{
			var playlist = await db.playlists.FindAsync(id);
			if (playlist != null)
				db.playlists.Remove(playlist);
		}

		public async Task<Playlist> GetById(int id)
		{
			return await db.playlists.FindAsync(id);
		}

		/// <summary>
		/// param name is user's login
		/// </summary>
		/// <param name="name"></param>
		/// <returns> The entity found, or null. </returns>
		public async Task<Playlist> GetByName(string name)
		{
			var playlist = await db.playlists.Where(p => p.Title == name).FirstOrDefaultAsync();
			return playlist;
		}

		public async Task<IEnumerable<Playlist>> ToList()
		{
			return await db.playlists.ToListAsync();
		}

		public void Update(Playlist item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}
