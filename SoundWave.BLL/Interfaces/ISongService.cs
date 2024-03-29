﻿using SoundWave.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Interfaces
{
	public interface ISongService
	{
		Task Create(SongDTO song);
		Task Update(SongDTO song);
		Task Delete(int id);
		Task<SongDTO> GetById(int id);
		Task<SongDTO> GetByName(string name);
		Task<IEnumerable<SongDTO>> ToList();

	}
}
