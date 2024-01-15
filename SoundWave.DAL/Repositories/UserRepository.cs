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
	public class UserRepository : IRepository<User>
	{
		private SoundWaveContext db;

		public UserRepository(SoundWaveContext context)
		{
			this.db = context;
		}

		public async Task Create(User item)
		{
			await db.users.AddAsync(item);
		}

		public async Task Delete(int id)
		{
			var user = await db.users.FindAsync(id);
			if(user != null)
				db.users.Remove(user);
		}

		public async Task<User> GetById(int id)
		{
			return await db.users.Where(u => u.Id == id).Include("playlists").FirstOrDefaultAsync();
		}

		/// <summary>
		/// param name is user's login
		/// </summary>
		/// <param name="name"></param>
		/// <returns> The entity found, or null. </returns>
		public async Task<User> GetByName(string name)
		{
			var user = await db.users.Where(u => u.login == name).Include("playlists").FirstOrDefaultAsync();
			return user;
		}

		public async Task<IEnumerable<User>> ToList()
		{
			return await db.users.ToListAsync();
		}

		public void Update(User item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}
