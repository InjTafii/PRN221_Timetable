using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.DTO;
using TimeTable_Project.DTO.File;
using TimeTable_Project.Mapping;
using TimeTable_Project.Models;
using TimeTable_Project.Service;
using System.Xml;
using static System.Reflection.Metadata.BlobBuilder;

namespace TimeTable_Project.Pages.Schedule
{
    public class CreateModel : PageModel
    {
        private readonly ScheduleManagementContext _context;

        public CreateModel(ScheduleManagementContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CSVModelDTO CSVData { get; set; }
        [BindProperty]
        public Models.Schedule Schedule { get; set; }
        public List<RoomDTO> Rooms { get; set; }
        public List<Models.Subject> Subjects { get; set; }
        public List<Models.Teacher> Teachers { get; set; }
        public List<Models.Class> Classes { get; set; }
        public List<Models.Slot> Slots { get; set; }

        public void OnGet()
        {
            GetData();
        }

        public void GetData()
        {
            RoomMapper mapper = new RoomMapper(_context);
            Rooms = mapper.ToRoomDTO();
            Subjects = _context.Subjects.ToList();
            Teachers = _context.Teachers.ToList();
            Classes = _context.Classes.ToList();
            Slots = _context.Slots.ToList();
        }

        public void OnPost()
        {
            ScheduleService createService = new ScheduleService(_context);
            string message = createService.AddDataToDB(CSVData);
            if (message == "Saved successfully!")
            {

                Response.Redirect("/Schedule/Index");
            }


            ViewData["Messages"] = message;
            GetData();
        }

    }
}