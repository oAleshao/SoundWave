using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class Playlist
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public ICollection<Song> songs { get; set; }
		public ICollection<User> users { get; set; }
		public User? Owner { get; set; }
		public bool isPublic { get; set; }
		public Playlist()
		{
			songs = new List<Song>();
			users = new List<User>();
		}
	}
}
