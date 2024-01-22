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
			User newUser = new User() { FullName = user.FullName, login = user.login, password = user.password };

			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
			var pbkdf2 = new Rfc2898DeriveBytes(user.password, salt, 100000);
			byte[] hash = pbkdf2.GetBytes(20);
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);
			string savedPasswordHash = Convert.ToBase64String(hashBytes);
			newUser.password = savedPasswordHash;
			newUser.salt = Encoding.UTF8.GetString(salt);

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
			var upUser = new User()
			{
				Id = user.Id,
				FullName = user.FullName,
				login = user.login,
				password = user.password,
				isAdmin = user.isAdmin,
			};

			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
			var pbkdf2 = new Rfc2898DeriveBytes(upUser.password, salt, 100000);
			byte[] hash = pbkdf2.GetBytes(20);
			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);
			string savedPasswordHash = Convert.ToBase64String(hashBytes);
			upUser.password = savedPasswordHash;
			upUser.salt = Encoding.UTF8.GetString(salt);

			Database.users.Update(upUser);
			await Database.Save();
		}
	}
}
