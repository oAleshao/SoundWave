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
	public class EFUnitOfWorks : IUnitOfWorks
	{
		private SoundWaveContext db;
		private GanreRepository ganreRepository;
		private PlaylistRepository playlistRepository;
		private SongRepository songRepository;
		private UserRepository userRepository;
		private UsersLikesRepository usersLikesRepository;

		public EFUnitOfWorks(SoundWaveContext db)
		{
			this.db = db;
		}

		public IRepository<User> users
		{
			get
			{
				if (userRepository == null)
					userRepository = new UserRepository(db);
				return userRepository;
			}
		}

		public IRepository<Ganre> ganres
		{
			get
			{
				if (ganreRepository == null)
					ganreRepository = new GanreRepository(db);
				return ganreRepository;
			}
		}

		public IRepository<Song> songs
		{
			get
			{
				if (songRepository == null)
					songRepository = new SongRepository(db);
				return songRepository;
			}
		}

		public IRepository<Playlist> playlists
		{
			get
			{
				if (playlistRepository == null)
					playlistRepository = new PlaylistRepository(db);
				return playlistRepository;
			}
		}

		public IRepositoryUserAction<UsersLikes> userLikes
		{
			get
			{
				if (usersLikesRepository == null)
					usersLikesRepository = new UsersLikesRepository(db);
				return usersLikesRepository;
			}
		}

		public async Task Save()
		{
			await db.SaveChangesAsync();
		}
	}
}
