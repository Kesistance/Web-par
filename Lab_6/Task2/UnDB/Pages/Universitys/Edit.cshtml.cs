﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UnDB.Data;
using UnDB.Models;

namespace UnDB.Pages.Universitys
{
    public class EditModel : PageModel
    {
        private readonly UnDB.Data.UnDBContext _context;

        public EditModel(UnDB.Data.UnDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public University University { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university =  await _context.University.FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }
            University = university;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(University).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UniversityExists(University.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UniversityExists(int id)
        {
            return _context.University.Any(e => e.Id == id);
        }
    }
}
