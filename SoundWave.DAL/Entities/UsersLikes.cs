using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Entities
{
	public class UsersLikes
	{
		public int Id { get; set; }
		public User? Owner { get; set; }
		public Song? song { get; set; }
		public bool Like { get; set; }
		public bool Dislike { get; set; }

	}
}
