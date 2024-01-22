
using SoundWave.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Models
{
    public class SongModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Executor { get; set; }
        public string? Href { get; set; }
        public string? videoHref { get; set; }
        public string? preview { get; set; }
        public int IdActiveUser { get; set; }

        public IEnumerable<GanreDTO>? songGanres { get; set; }
        public IEnumerable<GanreDTO>? ganres { get; set; }
        public IEnumerable<SongDTO>? songs { get; set; }
    }
}
