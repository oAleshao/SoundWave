using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class History
	{
		public int Id { get; set; }
		public User? user { get; set; }
		public Song? song { get; set; }
		public DateTime? date { get; set; }

	}
}
