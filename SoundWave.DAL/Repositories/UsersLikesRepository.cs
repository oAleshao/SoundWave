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
