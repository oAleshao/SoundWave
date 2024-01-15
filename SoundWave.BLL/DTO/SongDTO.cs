using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.DTO
{
	internal class SongDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Executor { get; set; }
		public UserDTO? Owner { get; set; }
		public ICollection<GanreDTO>? ganres { get; set; }
		public ICollection<PlaylistDTO>? playlists { get; set; }
		public string? Href { get; set; }
		public string? videoHref { get; set; }
		public string? preview { get; set; }
		public string duration { get; set; }
		public int amountViews { get; set; }
		public int Like { get; set; }
		public int Dislike { get; set; }
		public SongDTO()
		{
			ganres = new List<GanreDTO>();
			playlists = new List<PlaylistDTO>();
		}
	}
}
