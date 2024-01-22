using Microsoft.AspNetCore.Mvc;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using SoundWave.Models;
using System.Security.Cryptography;
using System.Text;

namespace SoundWave.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly ISongService songService;
        public AccountController(IUserService userService, ISongService songService)
        {
            this.userService = userService;
            this.songService = songService;
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckLogin(string? login)
        {
            var users = await userService.GetByName(login);
            if (users != null)
                return Json(false);
            return Json(true);
        }


        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([Bind("login", "password")] LoginModel loginUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var model = await userService.ToList();

                    if (model.Count() == 0)
                    {
                        ModelState.AddModelError("", "Такого пользователя в базе нету");
                        return View(loginUser);
                    }

                    var user = model.Where(u => u.login == loginUser.login).FirstOrDefault();

                    if (user == null)
                    {
                        ModelState.AddModelError("", "Такого пользователя в базе нету");
                        return View(loginUser);
                    }

                    if (user.Status)
                    {
                        string savedPasswordHash = user.password;
                        byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                        byte[] salt = new byte[16];
                        Array.Copy(hashBytes, 0, salt, 0, 16);
                        var pbkdf2 = new Rfc2898DeriveBytes(loginUser.password, salt, 100000);
                        byte[] hash = pbkdf2.GetBytes(20);
                        for (int i = 0; i < 20; i++)
                            if (hashBytes[i + 16] != hash[i])
                            {
                                ModelState.AddModelError("", "Данные введены не верно");
                                return View();
                            }
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(30);
                        Response.Cookies.Append("userLoginSoundWave", user.login, option);
                        Response.Cookies.Append("userFullNameSoundWave", user.FullName, option);
                        Response.Cookies.Append("userIsAdminSoundWave", user.isAdmin.ToString(), option);

                        HomeModel Hmodel = new HomeModel();
                        Hmodel.Songs = await songService.ToList();
                        Hmodel.currentSong = Hmodel.Songs.First();
                        Hmodel.previousSong = Hmodel.Songs.Last().Id;
                        Hmodel.nextSong = Hmodel.Songs.Skip(1).Take(1).FirstOrDefault().Id;
                        //Hmodel.playlists = await repository.GetListPlaylists();
                        return RedirectToAction("Index", "Home", Hmodel);
                    }
                    ModelState.AddModelError("", "Админ не подтвердил вашей регистрации");
                }
                catch (Exception ex)
                {
                    return View("Eror");
                }
            }
            return View();


        }

        public async Task<ActionResult> LogOut()
        {
            Response.Cookies.Delete("userLoginSoundWave");
            Response.Cookies.Delete("userFullNameSoundWave");
            Response.Cookies.Delete("userIsAdminSoundWave");
            HomeModel model = new HomeModel();
            model.Songs = await songService.ToList();
            model.currentSong = model.Songs.First();
            model.previousSong = model.Songs.Last().Id;
            model.nextSong = model.Songs.Skip(1).Take(1).FirstOrDefault().Id;
            //model.playlists = await repository.GetListPlaylists();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FullName", "login", "password", "confirmPassword")] RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserDTO newUser = new UserDTO() { FullName = user.FullName, login = user.login, password = user.password };

                    byte[] salt;
                    new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
                    var pbkdf2 = new Rfc2898DeriveBytes(user.password, salt, 100000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    byte[] hashBytes = new byte[36];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 20);
                    string savedPasswordHash = Convert.ToBase64String(hashBytes);
                    newUser.password = savedPasswordHash;
                    newUser.salt = Encoding.UTF8.GetString(salt);
                    await userService.Create(newUser);
                    return View("SignIn");
                }
                catch (Exception)
                {
                    return View("Eror");
                }
            }
            return View();
        }


        //public async Task<IActionResult> UsersStatus()
        //{
        //    var model = new UsersStatusModel();
        //    model.users = await repository.GetListUser();
        //    return View(model);
        //}

        //public async Task<IActionResult> ChooseUser(int id)
        //{
        //    var model = new UsersStatusModel();
        //    model.users = await repository.GetListUser();
        //    model.User = await repository.GetUserById(id);
        //    model.Id = id;
        //    return View("~/Views/Account/UsersStatus.cshtml", model);
        //}

        //public async Task<IActionResult> AuthorizeUser(int id)
        //{
        //    var model = new UsersStatusModel();
        //    model.users = await repository.GetListUser();
        //    model.User = await repository.GetUserById(id);
        //    model.User.Status = true;
        //    repository.updateUser(model.User);
        //    await repository.save();
        //    model.Id = id;
        //    return View("~/Views/Account/UsersStatus.cshtml", model);
        //}

    }
}
