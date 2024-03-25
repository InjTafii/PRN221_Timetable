using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using TimeTable_Project.DTO;
using TimeTable_Project.DTO.File;
using TimeTable_Project.Mapping;
using TimeTable_Project.Models;
using TimeTable_Project.Service;
using TimeTable_Project.DTO.File;
using TimeTable_Project.Mapping;
using TimeTable_Project.Models;
using TimeTable_Project.Service;

namespace TimeTable_Project.Pages.Schedule
{
    public class EditModel : PageModel
    {
        private readonly ScheduleManagementContext _context;

        public EditModel(ScheduleManagementContext context)
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
        [BindProperty]
        public int ScheduleId { get; set; }

        public void OnGet(int id)
        {
            GetData(id);
        }

        public void GetData(int id)
        {
            ScheduleId = id;
            Schedule = _context.Schedules.Include(s => s.Class).Include(s => s.Room).ThenInclude(r => r.Building).Include(s => s.Subject).Include(s => s.Slot).Include(s => s.Teacher).FirstOrDefault(s => s.Id == id);

            CSVData = new CSVModelDTO
            {
                Room = $"{Schedule.Room.Building.Code}-{Schedule.Room.Code}",
                Teacher = Schedule.Teacher.Code,
                Class = Schedule.Class.Code,
                Subject = Schedule.Subject.Code,
                TimeSlot = Schedule.Slot.SlotName
            };

            RoomMapper mapper = new RoomMapper(_context);
            Rooms = mapper.ToRoomDTO();
            Subjects = _context.Subjects.ToList();
            Teachers = _context.Teachers.ToList();
            Classes = _context.Classes.ToList();
            Slots = _context.Slots.ToList();
        }

        public void OnPost()
        {
            ScheduleService editService = new ScheduleService(_context);
            string message = editService.EditDataToDatabase(CSVData, ScheduleId);
            if (message == "Saved successfully!")
            {

                Response.Redirect("/Schedule/Index");
            }


            ViewData["Messages"] = message;
            GetData(ScheduleId);
        }

    }
}

