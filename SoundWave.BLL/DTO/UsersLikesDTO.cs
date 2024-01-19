﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.DTO
{
	public class UsersLikesDTO
	{
		public int Id { get; set; }
		public UserDTO? Owner { get; set; }
		public SongDTO? song { get; set; }
		public bool Like { get; set; }
		public bool Dislike { get; set; }
	}
}
