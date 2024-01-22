using System.ComponentModel.DataAnnotations;

namespace SoundWave.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string? login { get; set; }
        [Required(ErrorMessage = "обязательное поле")]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}
