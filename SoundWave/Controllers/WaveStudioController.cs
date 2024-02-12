using Microsoft.AspNetCore.Mvc;
using SoundWave.DAL.Interfaces;
using SoundWave.Filters;

namespace SoundWave.Controllers
{
    [Culture]
    public class WaveStudioController : Controller
    {
        public ActionResult Index()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View("Index");
        }

    }
}
