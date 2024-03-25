using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;
using TimeTable_Project.Service;

namespace TimeTable_Project.Pages.Schedule
{
    public class IndexModel : PageModel
    {
        private readonly ScheduleManagementContext _context;
        [BindProperty]
        public int ScheduleId { get; set; }
        public List<Models.Class> Classes { get; set; }
        public Models.Schedule Schedule { get; set; }
        public List<Models.Subject> Subjects { get; set; }
        public List<Models.Teacher> Teachers { get; set; }
        public List<Models.Slot> Slots { get; set; }
        public List<Models.Building> Buildings { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; }

        private int _totalItem, _pageSize;
        public int TotalPage { get; set; }

        public IndexModel(ScheduleManagementContext context)
        {
            _context = context;
        }

        public List<Models.Schedule> schedules { get; set; } = default!;

        public void OnGet(int id)
        {
            if (_context.Schedules != null)
            {
                schedules = _context.Schedules.Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
                Paging();

            }

        }
        public void Paging()
        {
            if (PageIndex < 1) PageIndex = 1;
            _totalItem = schedules.Count();
            _pageSize = 5;
            TotalPage = (int)Math.Ceiling((double)_totalItem / _pageSize);

            if (TotalPage > 0)
            {
                if (PageIndex > TotalPage) PageIndex = TotalPage;
                schedules = schedules.Skip((PageIndex - 1) * _pageSize)
                    .Take(_pageSize)
                    .ToList();
            }
        }

        public void OnPostDelete()
        {
            ScheduleService deleteService = new ScheduleService(_context);
            deleteService.DeleteToDB(ScheduleId);
            schedules = _context.Schedules.OrderByDescending(s => s.Id).Include(s => s.Class).Include(s => s.Subject).Include(s => s.Teacher).Include(s => s.Slot).Include(s => s.Room).ThenInclude(r => r.Building).ToList();
            Paging();
            Response.Redirect("/Schedule/Index");
        }
    }
}
