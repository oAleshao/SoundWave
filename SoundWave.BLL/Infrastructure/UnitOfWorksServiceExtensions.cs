using Microsoft.Extensions.DependencyInjection;
using SoundWave.DAL.Interfaces;
using SoundWave.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.BLL.Infrastructure
{
	public static class UnitOfWorksServiceExtensions
	{
		public static void AddUnitOfWorkService(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWorks, EFUnitOfWorks>();
		}
	}
}
