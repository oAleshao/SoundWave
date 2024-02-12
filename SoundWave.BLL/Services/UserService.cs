using AutoMapper;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Services
{
	public class UserService : IUserService
	{
		IUnitOfWorks Database { get; set; }

		public UserService(IUnitOfWorks unit)
		{
			Database = unit;
		}

		public async Task Create(UserDTO user)
		{
			User newUser = new User() { 
				FullName = user.FullName, 
				login = user.login, 
				password = user.password,
				Status = user.Status,
				salt = user.salt,
				isAdmin = user.isAdmin,
			};

			await Database.users.Create(newUser);
			await Database.Save();
		}

		public async Task Delete(int id)
		{
			await Database.users.Delete(id);
			await Database.Save();
		}

		public async Task<UserDTO> GetById(int id)
		{
			var user = await Database.users.GetById(id);
			return new UserDTO
			{
				Id = user.Id,
				FullName = user.FullName,
				login = user.login,
				password = user.password,
				isAdmin = user.isAdmin,
				Status = user.Status
			};
		}

		public async Task<UserDTO> GetByName(string name)
		{
			var user = await Database.users.GetByName(name);
			if (user == null)
				return null;
			return new UserDTO
			{
				Id = user.Id,
				FullName = user.FullName,
				login = user.login,
				password = user.password,
				isAdmin = user.isAdmin,
				salt = user.salt,
				Status = user.Status
			};
		}

		public async Task<IEnumerable<UserDTO>> ToList()
		{
			var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
			return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.users.ToList());
		}

		public async Task Update(UserDTO user)
		{
			var upUser = await Database.users.GetById(user.Id);

			upUser.Status = user.Status;

			Database.users.Update(upUser);
			await Database.Save();
		}
	}
}
