using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using SoundWave.Models;
using System.Diagnostics;

namespace SoundWave.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly IUserService userService;
        private readonly IGanreService ganreService;
        private readonly IWebHostEnvironment _appEnvironment;


        public SongsController(ISongService songService, IUserService userService, IGanreService ganreService, IWebHostEnvironment _appEnvironment)
        {
            this.songService = songService;
            this.userService = userService;
            this.ganreService = ganreService;
            this._appEnvironment = _appEnvironment;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var model = new SongModel();
            model.songs = await songService.ToList();
            model.ganres = await ganreService.ToList();
            var user = await userService.GetByName(Request.Cookies["userLoginSoundWave"]);
            if (user != null)
            {
                model.IdActiveUser = user.Id;
            }
            return View(model);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> Create([Bind("Id,Title,Executor,videoHref,Href,preview")] SongModel song, IFormFile? uploadedPrview, IFormFile? uploadedHref, IFormFile? uploadedVHref, string[] selectedGanres)
        {
            song = await InitModel(song);

            if (uploadedHref == null)
            {
                ModelState.AddModelError("Href", "Обязательное поле");
                return View("~/Views/Songs/Index.cshtml", song);
            }

            song.Href = "/songs/" + uploadedHref.FileName;

            if (uploadedPrview == null)
            {
                ModelState.AddModelError("preview", "Обязательное поле");
                return View("~/Views/Songs/Index.cshtml", song);
            }

            song.preview = "/previews/" + uploadedPrview.FileName;


            if (selectedGanres.Length == 0)
            {
                ModelState.AddModelError("songGanres", "Вы не указали жанр");
                return View("~/Views/Songs/Index.cshtml", song);
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
                    s.ganres.Add(await ganreService.GetByName(item));
                }
                TagLib.File mp3File = TagLib.File.Create(_appEnvironment.WebRootPath + pathHref);
                s.duration = mp3File.Properties.Duration.ToString("mm\\:ss");
                await songService.Create(s);
                return RedirectToAction(nameof(Index));
            }
            return View("~/Views/Songs/Index.cshtml", song);
        }

        private async Task<SongModel> InitModel(SongModel model)
        {
            model.songs = await songService.ToList();
            model.ganres = await ganreService.ToList();
            var user = await userService.GetByName(Request.Cookies["userLoginSoundWave"]);
            if (user != null)
            {
                model.IdActiveUser = user.Id;
            }
            return model;

        }

        //// GET: Songs/Edit/5
        //public async Task<IActionResult> Edit(int id)
        //{
        //    if (id == 0)
        //    {
        //        return View("~/Views/Shared/Error.cshtml");
        //    }

        //    var song = await repository.GetSongById(id);
        //    if (song == null)
        //    {
        //        return View("~/Views/Shared/Error.cshtml");
        //    }
        //    var model = new SongModel();
        //    model = await InitModel(model);
        //    model.Id = id;
        //    model.Title = song.Title;
        //    model.Executor = song.Executor;
        //    model.songGanres = song.ganres;
        //    model.Href = song.Href;
        //    model.videoHref = song.videoHref;
        //    model.preview = song.preview;

        //    return View(model);
        //}

        //// POST: Songs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[RequestSizeLimit(1000000000)]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Executor,Href,videoHref,Like,Dislike")] SongModel song, IFormFile? uploadedPrview, IFormFile? uploadedHref, IFormFile? uploadedVHref, string[] selectedGanres)
        //{
        //    song = await InitModel(song);
        //    var initModel = await repository.GetSongById(song.Id);
        //    song.Id = id;
        //    song.Title = initModel.Title;
        //    song.Executor = initModel.Executor;
        //    song.songGanres = initModel.ganres;
        //    song.Href = initModel.Href;
        //    song.videoHref = initModel.videoHref;
        //    song.preview = initModel.preview;

        //    if (id != song.Id)
        //    {
        //        return View("~/Views/Shared/Error.cshtml");
        //    }

        //    if (selectedGanres.Length == 0)
        //    {
        //        ModelState.AddModelError("songGanres", "Вы не указали жанр");
        //        return View(song);
        //    }



        //    if (ModelState.IsValid)
        //    {
        //        var changedSong = await repository.GetSongById(song.Id);
        //        if (uploadedHref != null)
        //        {
        //            changedSong.Href = "/songs/" + uploadedHref.FileName;
        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.Href, FileMode.Create))
        //            {
        //                await uploadedHref.CopyToAsync(fileStream);
        //            }
        //        }

        //        if (uploadedPrview != null)
        //        {
        //            changedSong.preview = "/previews/" + uploadedPrview.FileName;
        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.preview, FileMode.Create))
        //            {
        //                await uploadedPrview.CopyToAsync(fileStream);
        //            }
        //        }

        //        if (uploadedVHref != null)
        //        {
        //            changedSong.videoHref = "/videos/" + uploadedVHref.FileName;
        //            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + changedSong.videoHref, FileMode.Create))
        //            {
        //                await uploadedVHref.CopyToAsync(fileStream);
        //            }
        //        }

        //        changedSong.ganres = await repository.GetListGanres(selectedGanres);
        //        TagLib.File mp3File = TagLib.File.Create(_appEnvironment.WebRootPath + changedSong.Href);
        //        changedSong.duration = mp3File.Properties.Duration.ToString("mm\\:ss");
        //        repository.updateSong(changedSong);
        //        await repository.save();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(song);
        //}

        //// GET: Songs/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return View("~/Views/Shared/Error.cshtml");
        //    }

        //    var song = await repository.GetSongById(id);
        //    if (song == null)
        //    {
        //        return View("~/Views/Shared/Error.cshtml");
        //    }
        //    var model = new SongModel();
        //    model = await InitModel(model);
        //    model.Id = id;
        //    model.Title = song.Title;
        //    model.Executor = song.Executor;
        //    model.songGanres = song.ganres;
        //    model.Href = song.Href;
        //    model.videoHref = song.videoHref;
        //    model.preview = song.preview;

        //    return View(model);
        //}

        //// POST: Songs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (id != 0)
        //    {
        //        await repository.deleteSong(id);
        //    }

        //    await repository.save();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
