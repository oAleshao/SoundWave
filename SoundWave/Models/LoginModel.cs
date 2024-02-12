using SoundWave.Filters;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Login", ResourceType = typeof(Resources.Resource))]
        public string? login { get; set; }
        [Required(ErrorMessage = "обязательное поле")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        public string? password { get; set; }
    }
}
