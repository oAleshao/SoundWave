using AutoMapper;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Services
{
	public class UserLikesService : IUserLikesService
	{
		IUnitOfWorks Database { get; set; }

		public UserLikesService(IUnitOfWorks unit)
		{
			Database = unit;
		}


		public async Task Create(UsersLikesDTO usersLikesDTO)
		{
			var newUsersLikes = new UsersLikes()
			{
				Owner = await Database.users.GetById(usersLikesDTO.Owner.Id),
				song = await Database.songs.GetById(usersLikesDTO.song.Id),
				Dislike = usersLikesDTO.Dislike,
				Like = usersLikesDTO.Like,
			};
			await Database.userLikes.Create(newUsersLikes);
			await Database.Save();
		}

		public async Task<IEnumerable<UsersLikesDTO>> ToList(int id)
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UsersLikes, UsersLikesDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<UsersLikes>, IEnumerable<UsersLikesDTO>>(await Database.userLikes.ToList(id));
		}

		public async Task Update(UsersLikesDTO usersLikesDTO, string action, bool active)
		{
			await Database.userLikes.Update(usersLikesDTO.Owner.Id, usersLikesDTO.song.Id, action, active);
			await Database.Save();
		}
	}
}
