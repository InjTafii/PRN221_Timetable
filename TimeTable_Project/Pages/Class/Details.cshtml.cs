using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Class
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public DetailsModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

      public Models.Class GrClass { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var grclass = await _context.Classes.FirstOrDefaultAsync(m => m.Id == id);
            if (grclass == null)
            {
                return NotFound();
            }
            else 
            {
                GrClass = grclass;
            }
            return Page();
        }
    }
}
