using CsvHelper.Configuration;
using TimeTable_Project.DTO.File;

namespace TimeTable_Project.Mapping
{
    public class CSVModelMapping : ClassMap<CSVModelDTO>
    {
        public CSVModelMapping() 
        {
            Map(m => m.Class).Index(0);
            Map(m => m.Subject).Index(1);
            Map(m => m.Room).Index(2);
            Map(m => m.Teacher).Index(3);
            Map(m => m.TimeSlot).Index(4);
        }
    }
}
