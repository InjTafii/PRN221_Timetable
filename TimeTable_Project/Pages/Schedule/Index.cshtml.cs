using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly TimeTable_Project.Models.ScheduleManagementContext _context;
        [BindProperty]
        public int ScheduleId { get; set; }
        public List<Models.Class> Classes { get; set; }
        public Models.Schedule Schedule { get; set; }
        public List<Models.Subject> Subjects { get; set; }
        public List<Models.Teacher> Teachers { get; set; }
        public List<Models.Slot> Slots { get; set; }
        public List<Models.Building> Buildings { get; set; }
        public IndexModel(TimeTable_Project.Models.ScheduleManagementContext context)
        {
            _context = context;
        }

        public List<Models.Schedule> schedules { get; set; } = default!;

        public void OnGet(int id)
        {
            if (_context.Schedules != null)
            {
                schedules = _context.Schedules.Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
            }

        }
    }
}
