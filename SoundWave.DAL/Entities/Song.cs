using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class Song
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Executor { get; set; }
		public User? Owner { get; set; }
		public ICollection<Ganre>? ganres { get; set; }
		public ICollection<Playlist>? playlists { get; set; }
		public string? Href { get; set; }
		public string? videoHref { get; set; }
		public string? preview { get; set; }
		public string duration { get; set; }
		public int amountViews { get; set; }
		public int Like { get; set; }
		public int Dislike { get; set; }
		public Song()
		{
			ganres = new List<Ganre>();
			playlists = new List<Playlist>();
		}
	}
}
