using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundWave.DAL.Context;

namespace SoundWave.BLL.Infrastructure
{
	public static class SoundWaveExtensions
	{
		public static void AddSoccerContext(this IServiceCollection services, string connection)
		{
			services.AddDbContext<SoundWaveContext>(options => options.UseSqlServer(connection));
		}
	}
}
