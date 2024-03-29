﻿using Microsoft.EntityFrameworkCore;
using TimeTable_Project.Models;

namespace TimeTable_Project.Service
{
    public class ValidationService
    {
        private ScheduleManagementContext _context;

        public ValidationService(ScheduleManagementContext context)
        {
            _context = context;
        }

        public string CheckSlotAndRoom(Schedule schedule, int existedId)
        {
            var roomAndSlot = _context.Schedules
                                        .Include(s => s.Slot)
                                        .Include(s => s.Room)
                                            .ThenInclude(r => r.Building)
                                        .FirstOrDefault(s => s.SlotId == schedule.SlotId && s.RoomId == schedule.RoomId);
            if (roomAndSlot != null && roomAndSlot.Id != existedId)
            {
                return $"Error: Room {roomAndSlot.Room.Building.Code}-{roomAndSlot.Room.Code} has been used in slot {roomAndSlot.Slot.SlotName}.";

            }

            return "";
        }

        public string CheckSlotAndTeacher(Schedule schedule, int existedId)
        {
            var teacherAndSlot = _context.Schedules
                                        .Include(s => s.Slot)
                                        .Include(s => s.Teacher)
                                        .FirstOrDefault(s => s.SlotId == schedule.SlotId && s.TeacherId == schedule.TeacherId);
            if (teacherAndSlot != null && teacherAndSlot.Id != existedId)
            {
                return $"Error: Teacher {teacherAndSlot.Teacher.Code} has been booked in slot {teacherAndSlot.Slot.SlotName}.";

            }
            return "";
        }

        public string CheckSlotAndClass(Schedule schedule, int existedId)
        {
            var classAndSlot = _context.Schedules
                                       .Include(s => s.Slot)
                                       .Include(s => s.Class)
                                       .FirstOrDefault(s => s.SlotId == schedule.SlotId && s.ClassId == schedule.ClassId);
            if (classAndSlot != null && classAndSlot.Id != existedId)
            {
                return $"Error: Class {classAndSlot.Class.Code} has been used slot {classAndSlot.Slot.SlotName}";

            }
            return "";
        }

        public string CheckClassAndSubject(Schedule schedule, int existedId)
        {
            var classAndSubject = _context.Schedules
                                        .Include(s => s.Subject)
                                        .Include(s => s.Class)
                                        .FirstOrDefault(s => s.ClassId == schedule.ClassId && s.SubjectId == schedule.SubjectId);
            if (classAndSubject != null && classAndSubject.Id != existedId)
            {

                return $"Error: Class {classAndSubject.Class.Code} already have Subject {classAndSubject.Subject.Code}.";

            }

            return "";
        }
        internal string ValidateSchedule(Schedule schedule, int ScheduleId)
        {
            string message = "";

            message = CheckClassAndSubject(schedule, ScheduleId);
            if (message != "")
            {
                return message;
            }

            message = CheckSlotAndRoom(schedule, ScheduleId);
            if (message != "")
            {
                return message;
            }

            message = CheckSlotAndTeacher(schedule, ScheduleId);
            if (message != "")
            {
                return message;
            }

            message = CheckSlotAndClass(schedule, ScheduleId);
            if (message != "")
            {
                return message;
            }

            return message;
        }

        }
}
