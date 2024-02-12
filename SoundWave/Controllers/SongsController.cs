using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly IUserService userService;
        private readonly IGanreService ganreService;
        private readonly CreaterModel createrModel;
        private readonly IWebHostEnvironment _appEnvironment;


        public SongsController(ISongService songService, IUserService userService, IGanreService ganreService, IWebHostEnvironment _appEnvironment)
        {
            this.songService = songService;
            this.userService = userService;
            this.ganreService = ganreService;
            this._appEnvironment = _appEnvironment;
            createrModel = new CreaterModel(songService, userService, ganreService);

		}

      
        // GET: Songs
        public async Task<IActionResult> Index(string searchByTitle, string filterByGenre, 
            string filterByExecutor, SortState sortOrder, SongModel tmpModel, int page = 1)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var model = await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, sortOrder, null, tmpModel, Request.Cookies["userLoginSoundWave"], page);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> Create([Bind("Id,Title,Executor,videoHref,Href,preview")] SongModel song, IFormFile? uploadedPrview, IFormFile? uploadedHref, IFormFile? uploadedVHref, string[] selectedGanres,
			string? searchByTitle, string? filterByGenre, string? filterByExecutor, SortState? sortOrder, int page = 1)
        {
            var model = await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, song, Request.Cookies["userLoginSoundWave"], page);

            if (uploadedHref == null)
            {
                ModelState.AddModelError("Href", "Обязательное поле");
                return View("~/Views/Songs/Index.cshtml", model);
            }

            song.Href = "/songs/" + uploadedHref.FileName;

            if (uploadedPrview == null)
            {
                ModelState.AddModelError("preview", "Обязательное поле");
                return View("~/Views/Songs/Index.cshtml", model);
            }

            song.preview = "/previews/" + uploadedPrview.FileName;


            if (selectedGanres.Length == 0)
            {
                ModelState.AddModelError("songGanres", "Вы не указали жанр");
                return View("~/Views/Songs/Index.cshtml", model);
            }

            if (ModelState.IsValid)
            {

                string pathPreview = "/previews/" + uploadedPrview.FileName;
                string pathHref = "/songs/" + uploadedHref.FileName;
                string pathVideoHref = string.Empty;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathPreview, FileMode.Create))
                {
                    await uploadedPrview.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathHref, FileMode.Create))
                {
                    await uploadedHref.CopyToAsync(fileStream);
                }
                if (uploadedVHref != null)
                {
                    pathVideoHref = "/videos/" + uploadedVHref.FileName;
                    song.videoHref = "/videos/" + uploadedVHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathVideoHref, FileMode.Create))
                    {
                        await uploadedVHref.CopyToAsync(fileStream);
                    }
                }

                var s = new SongDTO()
                {
                    Title = song.Title,
                    Executor = song.Executor,
                    Href = pathHref,
                    videoHref = pathVideoHref,
                    preview = pathPreview,
                    OwnerId = userService.GetByName(Request.Cookies["userLoginSoundWave"]).Result.Id,
                };
                foreach (var item in selectedGanres)
                {
                    s.ganres.Add(await ganreService.GetById(int.Parse(item)));
                }
                TagLib.File mp3File = TagLib.File.Create(_appEnvironment.WebRootPath + pathHref);
                s.duration = mp3File.Properties.Duration.ToString("mm\\:ss");
                await songService.Create(s);
                return View("~/Views/Songs/Index.cshtml", await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, null, Request.Cookies["userLoginSoundWave"], page));
            }
            return View("~/Views/Songs/Index.cshtml", model);
        }

   

        public async Task<IActionResult> Edit(int id, string? searchByTitle, string? filterByGenre,
            string? filterByExecutor, SortState? sortOrder, int page = 1)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            var song = await songService.GetById(id);
            if (song == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            var tmpModel = new SongModel(); 
            tmpModel = await createrModel.InitModel(tmpModel, song);
            var model = await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, tmpModel, 
                Request.Cookies["userLoginSoundWave"], page);

            return View(model);
        }

        [HttpPost]
        [RequestSizeLimit(1000000000)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Executor,Href,videoHref,Like,Dislike")] SongModel song, 
            IFormFile? uploadedPrview, IFormFile? uploadedHref, IFormFile? uploadedVHref, string[] selectedGanres, string? searchByTitle, string? filterByGenre,
			string? filterByExecutor, SortState? sortOrder, int page = 1)
        {
            var model = await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, song, Request.Cookies["userLoginSoundWave"], page);
            if (id != song.Id)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            if (selectedGanres.Length == 0)
            {
                ModelState.AddModelError("songGanres", "Вы не указали жанр");
                return View(song);
            }



            if (ModelState.IsValid)
            {
                var changedSong = await songService.GetById(song.Id);
                if (uploadedHref != null)
                {
                    changedSong.Href = "/songs/" + uploadedHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.Href, FileMode.Create))
                    {
                        await uploadedHref.CopyToAsync(fileStream);
                    }
                }

                if (uploadedPrview != null)
                {
                    changedSong.preview = "/previews/" + uploadedPrview.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.preview, FileMode.Create))
                    {
                        await uploadedPrview.CopyToAsync(fileStream);
                    }
                }

                if (uploadedVHref != null)
                {
                    changedSong.videoHref = "/videos/" + uploadedVHref.FileName;
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.videoHref, FileMode.Create))
                    {
                        await uploadedVHref.CopyToAsync(fileStream);
                    }
                }
                changedSong.ganres = new List<GanreDTO>();
				foreach (var item in selectedGanres)
				{
					changedSong.ganres.Add(await ganreService.GetById(int.Parse(item)));
				}
                TagLib.File mp3File = TagLib.File.Create(_appEnvironment.WebRootPath + changedSong.Href);
                changedSong.duration = mp3File.Properties.Duration.ToString("mm\\:ss");
                changedSong.Title = song.Title;
                changedSong.Executor = song.Executor;
                await songService.Update(changedSong);
				return View("~/Views/Songs/Index.cshtml", await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, model, Request.Cookies["userLoginSoundWave"], page));

			}

			return View(song);
        }


        public async Task<IActionResult> Delete(int id, string searchByTitle, string filterByGenre,
			string filterByExecutor, SortState sortOrder, int page = 1)
        {
            HttpContext.Session.SetString("path", Request.Path);
            if (id == 0)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            var song = await songService.GetById(id);
            if (song == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            var tmpModel = new SongModel();
            tmpModel = await createrModel.InitModel(tmpModel, song);
			var model = await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, sortOrder, null, tmpModel, Request.Cookies["userLoginSoundWave"], page);

            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string? searchByTitle, string? filterByGenre,
			string? filterByExecutor, SortState? sortOrder, int page = 1)
        {
            if (id != 0)
            {
                await songService.Delete(id);
            }
			return View("~/Views/Songs/Index.cshtml", await createrModel.CreateModel(searchByTitle, filterByGenre, filterByExecutor, (SortState)sortOrder, null, null, Request.Cookies["userLoginSoundWave"], page));

		}


	}
}
