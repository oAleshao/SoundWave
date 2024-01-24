using SoundWave.BLL.DTO;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Models
{
    public class GanreModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string? Title { get; set; }
        public IEnumerable<GanreDTO>? ganres { get; set; }
    }
}
