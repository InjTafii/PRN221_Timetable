﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Building
{
    public class DeleteModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public DeleteModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.Building Building { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Buildings == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.FirstOrDefaultAsync(m => m.Id == id);

            if (building == null)
            {
                return NotFound();
            }
            else 
            {
                Building = building;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Buildings == null)
            {
                return NotFound();
            }
            var building = await _context.Buildings.FindAsync(id);

            if (building != null)
            {
                Building = building;
                _context.Buildings.Remove(Building);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
