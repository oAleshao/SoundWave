using Microsoft.AspNetCore.Mvc.Rendering;
using SoundWave.BLL.DTO;
using TagLib.Matroska;

namespace SoundWave.Models
{
	public class FilterViewModel
	{
		public List<string>? Executors { get; set; }
		public string? SelectedExecutor { get; set; }

		public List<GanreDTO>? Genres { get; set; }
		public string? SelectedGenres { get; set; }

		public string? SearchedData { get; set; }

		public FilterViewModel(string SelectedExecutor, List<GanreDTO> Genres, string SelectedGenres, string SearchedData)
		{
			this.Executors = new List<string>();
			this.SelectedExecutor = SelectedExecutor;
			this.Genres = Genres;
			this.SelectedGenres = SelectedGenres;
			this.SearchedData = SearchedData;
		}

		public FilterViewModel() { }
		
		public void SetExecutors(List<SongDTO> songs)
		{
			foreach (SongDTO song in songs)
			{
				var executors = song.Executor.Split([',', '&']);
				foreach(var ex in executors)
					if(!Contains(ex))
						Executors.Add(ex);
			}
		}

		private bool Contains(string executor)
		{
			foreach(var ex in Executors)
			{
				if(ex.ToLower() == executor.ToLower())
					return true;
			}
			return false;
		}
	}
}
