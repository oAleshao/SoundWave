using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using SoundWave.Filters;
using SoundWave.Models;
using System.Diagnostics;

namespace SoundWave.Controllers
{
	[Culture]
	public class HomeController : Controller
	{
		private readonly IPlaylistService playlistService;
		private readonly ISongService songService;
		private readonly IUserService userService;
        IHubContext<NotificationHub> hubContext { get; }
        public HomeController(IUserService userService, IPlaylistService playlistService, ISongService songService, IHubContext<NotificationHub> hub)
		{
			this.userService = userService;
			this.playlistService = playlistService;
			this.songService = songService;
            hubContext = hub;
        }

		public async Task<IActionResult> Index()
		{
            HttpContext.Session.SetString("path", Request.Path);
            HomeModel model = new HomeModel();
			model.Songs = await songService.ToList();
			model.currentSong = model.Songs.First();
			model.previousSong = model.Songs.Last().Id;
			model.nextSong = model.Songs.Skip(1).Take(1).FirstOrDefault().Id;
            //model.playlists = await playlistService.ToList();
            
            return View("Index", model);
		}

		public async Task<IActionResult> PlaySong(int id, bool UserChooseVideo, bool nextSongBtnJs)
		{
			HomeModel model = new HomeModel();
			model.UserChooseVideo = UserChooseVideo;
			model.Songs = await songService.ToList();
			//model.playlists = await playlistService.ToList();
			GetPrevCurNextSong(id, model);
            return View("Index", model);
		}

		public static void GetPrevCurNextSong(int id, HomeModel model)
		{
			List<SongDTO> songs = model.Songs.ToList();
			SongDTO currentSong = songs.Where(s => s.Id == id).FirstOrDefault();
			int indx = songs.IndexOf(currentSong);

			if (indx - 1 < 0)
				model.previousSong = songs.Last().Id;
			else
				model.previousSong = songs[indx - 1].Id;
			if (indx + 1 == songs.Count)
				model.nextSong = songs.First().Id;
			else
				model.nextSong = songs[indx + 1].Id;

			model.currentSong = currentSong;

		}

        private async Task SendMessage(string message)
        {
            // Вызов метода displayMessage на всех клиентах
            await hubContext.Clients.All.SendAsync("displayMessage", message);
        }

        public ActionResult ChooseCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "fr" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }
    }
}
