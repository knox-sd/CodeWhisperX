using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeWhisperX.Data;
using CodeWhisperX.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeWhisperX.Controllers
{
    public class CodicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CodicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Codices
        public async Task<IActionResult> Index()
        {
            return View(await _context.CodeX.ToListAsync());
        }
        // GET: Codices/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // Post: Codices/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        //public string ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.CodeX.Where( j => j.CodeQuestion.Contains(SearchPhrase)).ToListAsync());
            //return "You entered " + SearchPhrase;
        }

        // GET: Codices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeX = await _context.CodeX
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeX == null)
            {
                return NotFound();
            }

            return View(codeX);
        }

        // GET: Codices/Create

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Codices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodeQuestion,CodeAnswer")] CodeX codeX)
        {
            if (ModelState.IsValid)
            {
                _context.Add(codeX);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(codeX);
        }

        // GET: Codices/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeX = await _context.CodeX.FindAsync(id);
            if (codeX == null)
            {
                return NotFound();
            }
            return View(codeX);
        }

        // POST: Codices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodeQuestion,CodeAnswer")] CodeX codeX)
        {
            if (id != codeX.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(codeX);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CodeXExists(codeX.Id))
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
            return View(codeX);
        }

        // GET: Codices/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var codeX = await _context.CodeX
                .FirstOrDefaultAsync(m => m.Id == id);
            if (codeX == null)
            {
                return NotFound();
            }

            return View(codeX);
        }

        // POST: Codices/Delete/5 - Confirmed

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var codeX = await _context.CodeX.FindAsync(id);
            if (codeX != null)
            {
                _context.CodeX.Remove(codeX);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CodeXExists(int id)
        {
            return _context.CodeX.Any(e => e.Id == id);
        }
    }
}
