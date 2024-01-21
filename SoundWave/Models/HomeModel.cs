using SoundWave.BLL.DTO;

namespace SoundWave.Models
{
	public class HomeModel
	{
		public UserDTO? activeUser { get; set; }
		public IEnumerable<SongDTO>? Songs { get; set; }
		public IEnumerable<PlaylistDTO>? playlists { get; set; }
		public bool StatusCookie { get; set; }
		public int? previousSong { get; set; }
		public SongDTO? currentSong { get; set; }
		public int? nextSong { get; set; }
		public bool UserChooseVideo { get; set; }
		public bool nextBtnJsWork { get; set; }
	}
}
