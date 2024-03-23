using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Building
{
    public class IndexModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;

        public IndexModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public IList<Models.Building> Building { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Buildings != null)
            {
                Building = await _context.Buildings.ToListAsync();
            }
        }
    }
}
