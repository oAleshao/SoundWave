using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoundWave.BLL.DTO;
using SoundWave.BLL.Interfaces;
using SoundWave.DAL.Entities;
using SoundWave.DAL.Interfaces;
using SoundWave.Models;
using System.ComponentModel.DataAnnotations;

namespace SoundWave.Controllers
{
	public class GanresController : Controller
	{
		private readonly IGanreService ganreService;
		private readonly IUserService userService;

		public GanresController(IGanreService ganreService, IUserService userService)
		{
			this.ganreService = ganreService;
			this.userService = userService;
		}

		// GET: Ganres
		public async Task<IActionResult> Index()
		{
			GanreModel model = new GanreModel();
			model.ganres = await ganreService.ToList();
			return View(model);
		}


		// POST: Ganres/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title")] GanreModel ganre)
		{
			if (ModelState.IsValid)
			{
				var g = new GanreDTO();
				g.Title = ganre.Title;
				await ganreService.Create(g);
				return RedirectToAction(nameof(Index));
			}
			ganre.ganres = await ganreService.ToList();
			return View(ganre);
		}

		// GET: Ganres/Edit/5
		public async Task<IActionResult> Edit(int id)
		{
			if (id == 0)
			{
				return View("~/Views/Shared/Error.cshtml");
			}

			var ganre = await ganreService.GetById(id);
			if (ganre == null)
			{
				return View("~/Views/Shared/Error.cshtml");
			}
			return View(await createModel(ganre.Id, ganre.Title));
		}

		// POST: Ganres/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] GanreDTO ganre)
		{
			if (id != ganre.Id)
			{
				return View("~/Views/Shared/Error.cshtml");
			}

			if (ModelState.IsValid)
			{
				try
				{
					ganreService.Update(ganre);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!GanreExists(ganre.Id))
					{
						return View("~/Views/Shared/Error.cshtml");
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			
			return View(await createModel(ganre.Id, ganre.Title));
		}

		// GET: Ganres/Delete/5
		public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
			{
				return View("~/Views/Shared/Error.cshtml");
			}

			var ganre = await ganreService.GetById(id);
			if (ganre == null)
			{
				return View("~/Views/Shared/Error.cshtml");
			}
			return View(await createModel(ganre.Id, ganre.Title));
		}

		// POST: Ganres/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (id != 0)
			{
				await ganreService.Delete(id);
			}

			return RedirectToAction(nameof(Index));
		}

		private bool GanreExists(int id)
		{
			return ganreService.GetById(id) != null;
		}

		private async Task<GanreModel> createModel(int id, string title)
		{
			GanreModel model = new GanreModel();
			model.Id = id;
			model.Title = title;
			model.ganres = await ganreService.ToList();
			return model;
		}
	}
}
