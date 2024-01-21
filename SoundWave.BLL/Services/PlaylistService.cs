using AutoMapper;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Services
{
	public class PlaylistService : IPlaylistService
	{
		IUnitOfWorks Database { get; set; }

		public PlaylistService(IUnitOfWorks unit)
		{
			Database = unit;
		}

		public async Task Create(PlaylistDTO playlist)
		{
			var newPlaylist = new Playlist()
			{
				Title = playlist.Title,
				Description = playlist.Description,
				isPublic = playlist.isPublic,
				Owner = await Database.users.GetById(playlist.Owner.Id)
			};
			await Database.playlists.Create(newPlaylist);
			await Database.Save();
		}

		public async Task Delete(int id)
		{
			await Database.playlists.Delete(id);
		}

		//new UserDTO()
		//{
		//	Id = playlist.Owner.Id,
		//	FullName = playlist.Owner.FullName,
		//	login = playlist.Owner.login,
		//	password = playlist.Owner.password,
		//	isAdmin = playlist.Owner.isAdmin,
		//}
		public async Task<PlaylistDTO> GetById(int id)
		{
			var playlist = await Database.playlists.GetById(id);
			if (playlist == null)
				throw new ValidationException("Wrong playlist!");
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
			return new PlaylistDTO()
			{
				Title = playlist.Title,
				Description = playlist.Description,
				isPublic = playlist.isPublic,
				Owner = mapper.Map<User, UserDTO>(await Database.users.GetById(playlist.Owner.Id))

			};
		}

		public async Task<PlaylistDTO> GetByName(string name)
		{
			var playlist = await Database.playlists.GetByName(name);
			if (playlist == null)
				throw new ValidationException("Wrong playlist!");
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
			return new PlaylistDTO()
			{
				Title = playlist.Title,
				Description = playlist.Description,
				isPublic = playlist.isPublic,
				Owner = mapper.Map<User, UserDTO>(await Database.users.GetById(playlist.Owner.Id))
			};
		}

		public async Task<IEnumerable<PlaylistDTO>> ToList()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<Playlist, PlaylistDTO>()
		   .ForMember("Owner", opt => opt.MapFrom(c => c.Owner)).ForMember("songs", opt=>opt.MapFrom(c=>c.songs)));
			var mapper = new Mapper(config);
			return mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistDTO>>(await Database.playlists.ToList());
		}

		public async Task Update(PlaylistDTO playlist)
		{
			var upPlaylist = new Playlist()
			{
				Id = playlist.Id,
				Title = playlist.Title,
				Description = playlist.Description,
				isPublic = playlist.isPublic,
				Owner = await Database.users.GetById(playlist.Owner.Id)
			};
			Database.playlists.Update(upPlaylist);
			await Database.Save();
		}
	}
}
