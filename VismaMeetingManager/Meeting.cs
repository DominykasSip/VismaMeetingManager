using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaMeetingManager
{
    public class Meeting : BaseModel
    {
        public string ResponsiblePerson { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Attendants { get; set; }


        public Meeting(string name, string responsiblePerson, string description, string category, string type, DateTime startDate, DateTime endDate, string? id = null, List<string>? attendants = null ) : base(id, name)
        {
            ResponsiblePerson = responsiblePerson;
            Description = description;
            Category = category;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Attendants = attendants ?? new List<string>();
        }
    }
}
