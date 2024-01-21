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
	public class SongService : ISongService
	{
		IUnitOfWorks Database { get; set; }

		public SongService(IUnitOfWorks unit)
		{
			Database = unit;
		}

		public async Task Create(SongDTO song)
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<GanreDTO, Ganre>()).CreateMapper();
			var newSong = new Song()
			{
				Title = song.Title,
				Executor = song.Executor,
				ganres = mapper.Map<IEnumerable<GanreDTO>, IEnumerable<Ganre>>(song.ganres).ToList(),
				Owner = await Database.users.GetById(song.OwnerId),
				Href = song.Href,
				videoHref = song.videoHref,
				preview = song.preview,
				duration = song.duration,
			};
			await Database.songs.Create(newSong);
			await Database.Save();
		}

		public async Task Delete(int id)
		{
			await Database.songs.Delete(id);
			await Database.Save();
		}


		//new UserDTO() { 
		//	Id = song.Owner.Id,
		//	FullName = song.Owner.FullName,
		//	login = song.Owner.login,
		//	password = song.Owner.password,
		//	isAdmin = song.Owner.isAdmin,
		//}
		public async Task<SongDTO> GetById(int id)
		{
			var song = await Database.songs.GetById(id);
			if (song == null)
				throw new ValidationException("Wrong song!");
			return new SongDTO
			{
				Id = song.Id,
				Title = song.Title,
				Executor = song.Executor,
				OwnerId = song.Owner.Id
			};
		}

		public async Task<SongDTO> GetByName(string name)
		{
			var song = await Database.songs.GetByName(name);
			if (song == null)
				throw new ValidationException("Wrong song!");
			return new SongDTO
			{
				Id = song.Id,
				Title = song.Title,
				Executor = song.Executor,
				OwnerId = song.Owner.Id
			};
		}

		public async Task<IEnumerable<SongDTO>> ToList()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
		   .ForMember("OwnerId", opt => opt.MapFrom(c => c.Owner.Id)).ForMember("ganres", opt => opt.MapFrom(c => c.ganres))); ;
			var mapper = new Mapper(config);
			return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.songs.ToList());
		}

		public async Task Update(SongDTO song)
		{
			var upSong = new Song()
			{
				Id = song.Id,
				Title = song.Title,
				Executor = song.Executor,
				Owner = await Database.users.GetById(song.OwnerId)
			};
			Database.songs.Update(upSong);
			await Database.Save();
		}
	}
}
