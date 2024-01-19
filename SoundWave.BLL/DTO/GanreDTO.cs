using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.DTO
{
	public class GanreDTO
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public ICollection<SongDTO>? songs { get; set; }
	}
}
