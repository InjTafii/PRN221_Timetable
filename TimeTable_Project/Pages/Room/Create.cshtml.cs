using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Room
{
    public class CreateModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public CreateModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Models.Room Room { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Rooms == null || Room == null)
            {
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
