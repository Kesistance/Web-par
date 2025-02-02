﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UnDB.Data;
using UnDB.Models;

namespace UnDB.Pages.Universitys
{
    public class DeleteModel : PageModel
    {
        private readonly UnDB.Data.UnDBContext _context;

        public DeleteModel(UnDB.Data.UnDBContext context)
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

            var university = await _context.University.FirstOrDefaultAsync(m => m.Id == id);

            if (university == null)
            {
                return NotFound();
            }
            else
            {
                University = university;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University.FindAsync(id);
            if (university != null)
            {
                University = university;
                _context.University.Remove(University);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
