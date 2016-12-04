using System;
using System.Collections.Generic;
using SqlConferenceManagementSystemGenerator.Consts;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class WorkshopReservationModel
    {
        public int ReservationId { get; set; }
        public int ClientId { get; set; }
        public int WorkshopId { get; set; }
        public int NumberOfSeats { get; set; }
        public ReservationState State { get; set; }
        public double TotalPrice { get; set; }
        public double PayedAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ParticipantModel> Participants { get; set; }
    }
}
