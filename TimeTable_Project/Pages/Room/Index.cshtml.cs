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
    public class IndexModel : PageModel
    {
        private readonly ScheduleManagementContext _context;

        public IndexModel(ScheduleManagementContext context)
        {
            _context = context;
        }

        public IList<Models.Room> Room { get;set; } = default!;
        public int BuildingId { get; set; }
        public IList<Models.Building> AvailableBuildings { get; set; } = default!;
        public async Task OnGetAsync()
        {
            AvailableBuildings = await _context.Buildings.ToListAsync();

            if (BuildingId > 0)
            {
                Room = await _context.Rooms
                  .Include(r => r.Building)
                  .Where(r => r.BuildingId == BuildingId)
                  .ToListAsync();
            }
            else
            {
                Room = await _context.Rooms
                  .Include(r => r.Building)
                  .ToListAsync();
            }
        }
    }
}
