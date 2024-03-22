using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Room
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public DetailsModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

      public Models.Room Room { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            else 
            {
                Room = room;
            }
            return Page();
        }
    }
}
