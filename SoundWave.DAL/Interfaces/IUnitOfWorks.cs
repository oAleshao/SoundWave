using SoundWave.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Interfaces
{
	public interface IUnitOfWorks
	{
		public IRepository<User> users { get; }
		public IRepository<Ganre> ganres { get; }
		public IRepository<Song> songs { get; }
		public IRepository<Playlist> playlists { get; }
		public IRepositoryUserAction<UsersLikes> userLikes { get; }
		public Task Save();
	}
}
