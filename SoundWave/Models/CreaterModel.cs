using Azure.Core;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.BLL.Services;

namespace SoundWave.Models
{
	public class CreaterModel
	{
		private readonly ISongService songService;
		private readonly IUserService userService;
		private readonly IGanreService ganreService;

		public CreaterModel(ISongService songService, IUserService userService, IGanreService ganreService)
		{
			this.songService = songService;
			this.userService = userService;
			this.ganreService = ganreService;
		}

		public async Task<SongModel> CreateModel(string searchByTitle, string filterByGenre,
		  string filterByExecutor, SortState sortOrder, SongDTO song, SongModel tmpModel, string userName, int page = 1)
		{
			var model = new SongModel();
			int pageSize = 10;
			model.songs = await songService.ToList();
			model.ganres = await ganreService.ToList();
			var user = await userService.GetByName(userName);
			if (user != null)
			{
				model.IdActiveUser = user.Id;
			}

			model.SortViewModel = new SortViewModel(sortOrder);
			model.FilterViewModel = new FilterViewModel(filterByExecutor, ganreService.ToList().Result.ToList(), filterByGenre, searchByTitle);
			model.FilterViewModel.SetExecutors(songService.ToList().Result.ToList());

			if (searchByTitle != string.Empty && searchByTitle != null)
				model.songs = model.songs.Where(s => s.Title.ToLower().Contains(searchByTitle.ToLower()));
			if (filterByExecutor != string.Empty && filterByExecutor != null)
				model.songs = model.songs.Where(s => s.Executor.ToLower().Contains(filterByExecutor.ToLower()));
			if (filterByGenre != string.Empty && filterByGenre != null)
				model.songs = model.songs.Where(s => s.HasGenre(filterByGenre) == true);

			model.songs = sortOrder switch
			{
				SortState.TitleDesc => model.songs.OrderByDescending(s => s.Title),
				_ => model.songs.OrderBy(s => s.Title),
			};

			var count = model.songs.Count();
			model.songs = model.songs.Skip((page - 1) * pageSize).Take(pageSize).ToList();
			model.PageViewModel = new PageViewModel(count, page, pageSize);

			if (song != null)
				model = await InitModel(model, song);
			else if (tmpModel != null && tmpModel.Title != null)
				model = await InitModel(model, tmpModel);

			return model;

		}

		public async Task<SongModel> InitModel(SongModel mainModel, SongModel model)
		{
			mainModel.Id = model.Id;
			mainModel.Title = model.Title;
			mainModel.Executor = model.Executor;
			mainModel.songGanres = model.songGanres;
			mainModel.Href = model.Href;
			mainModel.videoHref = model.videoHref;
			mainModel.preview = model.preview;
			mainModel.ganres = await ganreService.ToList();
			return mainModel;
		}

		public async Task<SongModel> InitModel(SongModel mainModel, SongDTO model)
		{
			mainModel.Id = model.Id;
			mainModel.Title = model.Title;
			mainModel.Executor = model.Executor;
			mainModel.songGanres = model.ganres;
			mainModel.Href = model.Href;
			mainModel.videoHref = model.videoHref;
			mainModel.preview = model.preview;
			mainModel.ganres = await ganreService.ToList();
			return mainModel;
		}

		
	}
}
