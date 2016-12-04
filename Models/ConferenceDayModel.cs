using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class ConferenceDayModel
    {
        public int DayNum { get; set; }
        public List<WorkshopModel> Workshops { get; set; }
        public int ParticipantsNumber {get;set;}
        public List<ConferenceReservationModel> Reservations { get; set; }

        public ConferenceDayModel()
        {
            Workshops = new List<WorkshopModel>();
        }
    }
}
