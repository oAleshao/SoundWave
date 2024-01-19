using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.DTO
{
	public class PlaylistDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public ICollection<SongDTO> songs { get; set; }
		public ICollection<UserDTO> users { get; set; }
		public UserDTO? Owner { get; set; }
		public bool isPublic { get; set; }
		public PlaylistDTO()
		{
			songs = new List<SongDTO>();
			users = new List<UserDTO>();
		}
	}
}
