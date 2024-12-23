﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiemsach.Data;

namespace tiemsach.Controllers
{
    public class NxbsController : Controller
    {
        private readonly TiemsachContext _context;

        public NxbsController(TiemsachContext context)
        {
            _context = context;
        }

        // GET: Nxbs
        public async Task<IActionResult> Index()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View(await _context.Nxbs.ToListAsync());
        }

        // GET: Nxbs/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nxb == null)
            {
                return NotFound();
            }

            return View(nxb);
        }

        // GET: Nxbs/Create
        public IActionResult Create()
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return View();
        }

        // POST: Nxbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Diachi,Tinhtrang")] Nxb nxb)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            nxb.Tinhtrang = true;
            if (ModelState.IsValid)
            {
                _context.Add(nxb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nxb);
        }

        // GET: Nxbs/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs.FindAsync(id);
            if (nxb == null)
            {
                return NotFound();
            }
            return View(nxb);
        }

        // POST: Nxbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Ten,Diachi,Tinhtrang")] Nxb nxb)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id != nxb.Id)
            {
     
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nxb.Tinhtrang = true;
                    _context.Update(nxb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NxbExists(nxb.Id))
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
            return View(nxb);
        }

        // GET: Nxbs/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var nxb = await _context.Nxbs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nxb == null)
            {
                return NotFound();
            }

            return View(nxb);
        }

        // POST: Nxbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            var nxb = await _context.Nxbs.FindAsync(id);
            if (nxb != null)
            {
                try
                {
                    nxb.Tinhtrang = false;
                    _context.Update(nxb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NxbExists(nxb.Id))
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

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NxbExists(long id)
        {
            ViewData["Layout"] = "_LayoutAdmin";
            return _context.Nxbs.Any(e => e.Id == id);
        }
    }
}
