using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BE.Example.Data;
using BE.Example.Models;
using BE.Example.Extensions;
using BE.Example.ViewModels;

namespace BE.Example.Controllers
{
    public class LiteralTranslationsController : Controller
    {
        private readonly ExampleDBContext _context;

        public LiteralTranslationsController(ExampleDBContext context)
        {
            _context = context;
        }

        //C.2
        public async Task<IActionResult> Translations(int moduleId, int languageId, int countryId)
        {
//#if DEBUG
            //Solo para testing
            moduleId = 1;
            countryId = 1;
            languageId = 1;
//#endif

            var literals =
                 _context
                        .LiteralTranslations
                        .Include(x => x.Literal)
                            .ThenInclude(x => x.Module)
                        .Where(x => x.CountryId == countryId && x.LanguageId == languageId && x.Literal.ModuleId == moduleId);

            //TODO: Se debe hacer 'Union' con la tabla Literals para obtener todos los literales no traducidos

            string query = literals.ToSql();
            Console.WriteLine(query);

            return View(await literals.ToListAsync());
        }

        //C.3
        public async Task<IActionResult> Status()
        {
            var Status =
                    _context
                        .LiteralTranslations
                            .Include(x => x.Literal)
                            .Include(x => x.Language)
                            .Include(x => x.Country)
                            .GroupBy(x => new { x.Language, x.Country })
                            .Select(g => new StatusVM
                            {
                                Language = g.Key.Language,
                                Country = g.Key.Country,
                                Translated = g.Count(),
                                InReview = _context.LiteralTranslations.Where(x => x.InReview == true).Count()
                            });

            //string query = Status.ToSql();
            //Console.WriteLine(query);

            return View(await Status.ToListAsync());
        }

        //C.4
        public async Task<IActionResult> InReview()
        {
            var InReview =  
                    _context
                        .LiteralTranslations
                            .Include(x => x.Literal)
                            .Include(x => x.Language)
                            .Include(x => x.Country)
                            .Where(x => x.InReview == true)
                            .Select(x => new InReviewVM
                            {
                                Language = x.Language.Name,
                                Country = x.Country == null ? "General" : x.Country.Name,
                                Literal = x.Literal.Code
                            });

            string query = InReview.ToSql();
            Console.WriteLine(query);

            return View(await InReview.ToListAsync());
        }

        public async Task<IActionResult> Index(int? moduleId, int? countryId, int? languageId)
         {
            moduleId = 1;
            countryId = 1;
            languageId = 1;

            var exampleDBContext = _context.Literals
                                                .Include(l => l.Module);

            //string query = exampleDBContext.ToSql();
            //Console.WriteLine(query);

            return View(await exampleDBContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var literalTranslation = await _context.LiteralTranslations
                .Include(l => l.Country)
                .Include(l => l.Language)
                .Include(l => l.Literal)
                .FirstOrDefaultAsync(m => m.LiteralTranslationId == id);
            if (literalTranslation == null)
            {
                return NotFound();
            }

            return View(literalTranslation);
        }

        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId");
            ViewData["LiteralId"] = new SelectList(_context.Literals, "LiteralId", "LiteralId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LiteralTranslationId,LanguageId,CountryId,LiteralId,ValueZero,ValueOne,ValueMany,InReview")] LiteralTranslation literalTranslation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(literalTranslation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", literalTranslation.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", literalTranslation.LanguageId);
            ViewData["LiteralId"] = new SelectList(_context.Literals, "LiteralId", "LiteralId", literalTranslation.LiteralId);
            return View(literalTranslation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var literalTranslation = await _context.LiteralTranslations.FindAsync(id);
            if (literalTranslation == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", literalTranslation.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", literalTranslation.LanguageId);
            ViewData["LiteralId"] = new SelectList(_context.Literals, "LiteralId", "LiteralId", literalTranslation.LiteralId);
            return View(literalTranslation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LiteralTranslationId,LanguageId,CountryId,LiteralId,ValueZero,ValueOne,ValueMany,InReview")] LiteralTranslation literalTranslation)
        {
            if (id != literalTranslation.LiteralTranslationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(literalTranslation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LiteralTranslationExists(literalTranslation.LiteralTranslationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "CountryId", "CountryId", literalTranslation.CountryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "LanguageId", "LanguageId", literalTranslation.LanguageId);
            ViewData["LiteralId"] = new SelectList(_context.Literals, "LiteralId", "LiteralId", literalTranslation.LiteralId);
            return View(literalTranslation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var literalTranslation = await _context.LiteralTranslations
                .Include(l => l.Country)
                .Include(l => l.Language)
                .Include(l => l.Literal)
                .FirstOrDefaultAsync(m => m.LiteralTranslationId == id);
            if (literalTranslation == null)
            {
                return NotFound();
            }

            return View(literalTranslation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var literalTranslation = await _context.LiteralTranslations.FindAsync(id);
            _context.LiteralTranslations.Remove(literalTranslation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LiteralTranslationExists(int id)
        {
            return _context.LiteralTranslations.Any(e => e.LiteralTranslationId == id);
        }
    }
}
