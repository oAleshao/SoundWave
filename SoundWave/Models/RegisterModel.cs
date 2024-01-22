﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "обязательное поле")]
        [RegularExpression(@"[^!@#$%^&*()\\\.\-=[\]{}/<>~+|0-9]{1,150}", ErrorMessage = "Некорректные данные")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        [Remote(action: "CheckLogin", controller: "Account", ErrorMessage = "Логин уже используется")]
        [RegularExpression(@"[^!@#$%^&*()\\\.\-=[\]{}/<>~+|]{1,100}", ErrorMessage = "Некорректные данные")]
        public string? login { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        [RegularExpression(@"[$A-Z]+[A-Za-z0-9]{5,100}", ErrorMessage = "Некорректные данные")]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? confirmPassword { get; set; }
    }
}

