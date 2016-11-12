using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class WorkshopModel
    {
        public int WorkshopId { get; set; }
        public int ConferenceId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserStamp { get; set; }
        public int MaxParticipantNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal BasePrice { get; set; }
        public string Description { get; set; }
    }
}
