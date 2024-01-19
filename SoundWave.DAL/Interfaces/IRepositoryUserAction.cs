using SoundWave.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Interfaces
{
	public interface IRepositoryUserAction<T> where T : class
	{
		Task Create(T item);
		Task Update(int userId, int songId, string action, bool active);
		Task<IEnumerable<T>> ToList(int id);
	}
}
