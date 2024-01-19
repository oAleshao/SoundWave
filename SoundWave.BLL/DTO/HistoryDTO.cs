using SoundWave.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.DTO
{
	public class HistoryDTO
	{
		public int Id { get; set; }
		public UserDTO? user { get; set; }
		public SongDTO? song { get; set; }
		public DateTime? date { get; set; }
	}
}
