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
		Task<IEnumerable<T>> ToList(int id);
	}
}
