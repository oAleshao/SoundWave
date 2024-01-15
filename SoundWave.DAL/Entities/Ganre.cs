using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class Ganre
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public ICollection<Song>? songs { get; set; }
	}
}
