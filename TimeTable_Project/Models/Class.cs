using System;
using System.Collections.Generic;

namespace TimeTable_Project.Models
{
    public partial class Class
    {
        public Class()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int Id { get; set; }
        public string Code { get; set; } = null!;

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
