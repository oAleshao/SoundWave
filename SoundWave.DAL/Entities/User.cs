using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string? FullName { get; set; }
		public string? login { get; set; }
		public string? password { get; set; }
		public string? salt { get; set; }
		public bool isAdmin { get; set; }
		public bool Status { get; set; }
		public ICollection<Playlist> playlists { get; set; }
		public User() 
		{
			playlists = new List<Playlist>();
		}
	}
}
