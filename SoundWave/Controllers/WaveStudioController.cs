using Microsoft.AspNetCore.Mvc;
using SoundWave.DAL.Interfaces;

namespace SoundWave.Controllers
{
    public class WaveStudioController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

    }
}
