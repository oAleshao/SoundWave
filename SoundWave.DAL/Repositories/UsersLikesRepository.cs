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
	public class UsersLikesRepository : IRepositoryUserAction<UsersLikes>
	{
		private SoundWaveContext db;

		public UsersLikesRepository(SoundWaveContext context)
		{
			this.db = context;
		}

		public async Task Create(UsersLikes item)
		{
		   await db.usersLikes.AddAsync(item);
		}

		public async Task Update(int userId, int songId, string action, bool active)
		{
			var userLikes = await db.usersLikes.Where(ul => ul.Owner.Id == userId && ul.song.Id == songId).FirstOrDefaultAsync();
			if (userLikes != null)
			{
				if(action == "like")
					userLikes.Like = active;
				else
					userLikes.Dislike = active;
			}
			db.Entry(userLikes).State = EntityState.Modified;
		}

		/// <summary>
		/// param id is user's id who pressed
		/// </summary>
		/// <param name="id"></param>
		/// <returns> The entity found, or null. </returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<IEnumerable<UsersLikes>> ToList(int id)
		{
			var listLikes = await db.usersLikes.Where(l => l.Owner.Id == id).ToListAsync();
			return listLikes;
		}

		
	}
}
