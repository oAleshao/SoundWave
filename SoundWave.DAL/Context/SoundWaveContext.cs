using Microsoft.EntityFrameworkCore;
using SoundWave.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SoundWave.DAL.Context
{
	public class SoundWaveContext : DbContext
	{
		public DbSet<User> users { get; set; }
		public DbSet<Ganre> ganres { get; set; }
		public DbSet<Song> songs { get; set; }
		public DbSet<Playlist> playlists { get; set; }
		public DbSet<UsersLikes> usersLikes { get; set; }
		public DbSet<History> histories { get; set; }

		public SoundWaveContext(DbContextOptions<SoundWaveContext> options) : base(options)
		{
			if (Database.EnsureCreated())
			{
				User user = new User()
				{
					FullName = "Alex Salnik",
					login = "admin",
					password = "admin",
					isAdmin = true,
					Status = true
				};

				byte[] salt;
				new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
				var pbkdf2 = new Rfc2898DeriveBytes(user.password, salt, 100000);
				byte[] hash = pbkdf2.GetBytes(20);
				byte[] hashBytes = new byte[36];
				Array.Copy(salt, 0, hashBytes, 0, 16);
				Array.Copy(hash, 0, hashBytes, 16, 20);
				string savedPasswordHash = Convert.ToBase64String(hashBytes);
				user.password = savedPasswordHash;
				user.salt = Encoding.UTF8.GetString(salt);

				users?.Add(user);


				ganres?.Add(new Ganre()
				{
					Title = "Хип-поп"
				});

				ganres?.Add(new Ganre()
				{
					Title = "Кантри"
				});

				ganres?.Add(new Ganre()
				{
					Title = "Рок"
				});

				ganres?.Add(new Ganre()
				{
					Title = "Классика"
				});

				songs?.Add(new Song()
				{
					Title = "Заново завоевать",
					Executor = "Pepel Nahudi",
					Href = "/songs/pepel-nahudi-zanovo-zavoevat-mp3.mp3",
					preview = "/previews/Screenshot 2024-01-04 072921.png",
					duration = "01:53",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "Лига Опасного Интернета",
					Executor = "Oxxxymiron",
					Href = "/songs/oxxxymiron-liga-opasnogo-interneta-mp3.mp3",
					videoHref = "/videos/LOIOxxymiron.mp4",
					preview = "/previews/Screenshot 2024-01-05 045023.png",
					duration = "02:20",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "Калипсо",
					Executor = "XOLIDAYBOY",
					Href = "/songs/xolidayboy-kalipso-mp3.mp3",
					preview = "/previews/Screenshot 2024-01-05 045746.png",
					duration = "02:12",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "girls (feat. Horsehead)",
					Executor = "Lil Peep",
					Href = "/songs/lil-peep-girls-feat-horsehead.mp3",
					videoHref = "/videos/GirlsLilPeep.mp4",
					preview = "/previews/Screenshot 2024-01-05 045955.png",
					duration = "03:55",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "PAYPASS",
					Executor = "WhyBaby?",
					Href = "/songs/whybaby-paypass-mp3.mp3",
					videoHref = "/videos/PayPassWhyBaby.mp4",
					preview = "/previews/Screenshot 2024-01-05 050415.png",
					duration = "02:14",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "I Mean It (feat. Remo)",
					Executor = "G-Eazy",
					Href = "/songs/geazy-feat-remo-i-mean-it_(muzzona.kz).mp3",
					videoHref = "/videos/IMeanIt.mp4",
					preview = "/previews/Screenshot 2024-01-05 050704.png",
					duration = "03:56",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "Cheat Code",
					Executor = "Sollar",
					Href = "/songs/sollar-cheat-code-iz-t-s-mazhor-2-mp3.mp3",
					preview = "/previews/Screenshot 2024-01-05 050933.png",
					duration = "04:07",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "On My Own",
					Executor = "Darci",
					Href = "/songs/darci-on-my-own-mp3.mp3",
					preview = "/previews/Screenshot 2024-01-05 051224.png",
					duration = "02:51",
					Owner = user,
				});

				songs?.Add(new Song()
				{
					Title = "Вся моя",
					Executor = "Diazz",
					Href = "/songs/diazz-vsya-moya-mp3.mp3",
					preview = "/previews/Screenshot 2024-01-05 051409.png",
					duration = "02:00",
					Owner = user,
				});

				SaveChanges();
			}
		}

	}
}
