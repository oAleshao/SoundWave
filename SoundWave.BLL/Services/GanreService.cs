using AutoMapper;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Services
{
	public class GanreService : IGanreService
	{
		IUnitOfWorks Database { get; set; }

		public GanreService(IUnitOfWorks unit)
		{
			Database = unit;
		}

		public async Task Create(GanreDTO ganre)
		{
			var newGanre = new Ganre()
			{
				Title = ganre.Title
			};
		    await Database.ganres.Create(newGanre);
			await Database.Save();
		}

		public async Task Delete(int id)
		{
			await Database.ganres.Delete(id);
			await Database.Save();
		}

		public async Task<GanreDTO> GetById(int id)
		{
			var ganre = await Database.ganres.GetById(id);
			if(ganre == null)
				throw new ValidationException("Wrong ganre!");
			return new GanreDTO
			{
				Id = ganre.Id,
				Title = ganre.Title,
			};

		}

		public async Task<GanreDTO> GetByName(string name)
		{
			var ganre = await Database.ganres.GetByName(name);
			if (ganre == null)
				throw new ValidationException("Wrong ganre!");
			return new GanreDTO
			{
				Id = ganre.Id,
				Title = ganre.Title,
			};

		}

		public async Task<IEnumerable<GanreDTO>> ToList()
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Ganre, GanreDTO>()).CreateMapper();
			return  mapper.Map<IEnumerable<Ganre>, IEnumerable<GanreDTO>>(await Database.ganres.ToList());
		}

		public async Task Update(GanreDTO ganre)
		{
			var upGanre = new Ganre()
			{
				Id = ganre.Id,
				Title = ganre.Title,
			};
		    Database.ganres.Update(upGanre);
			await Database.Save();
		}
	}
}
