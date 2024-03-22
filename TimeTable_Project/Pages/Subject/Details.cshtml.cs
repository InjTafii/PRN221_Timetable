using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Subject
{
    public class DetailsModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public DetailsModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

      public Models.Subject Subject { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Subjects == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FirstOrDefaultAsync(m => m.Id == id);
            if (subject == null)
            {
                return NotFound();
            }
            else 
            {
                Subject = subject;
            }
            return Page();
        }
    }
}
