using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Interfaces
{
	public interface IUsersHistory<T> where T : class
	{
		Task Create(T item);
		Task Delete(int id);
		Task<IEnumerable<T>> ToList();

	}
}
