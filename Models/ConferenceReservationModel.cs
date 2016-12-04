using SqlConferenceManagementSystemGenerator.Consts;
using System;
using System.Collections.Generic;
namespace SqlConferenceManagementSystemGenerator.Models
{
    public class ConferenceReservationModel
    {
        public int ReservationId { get; set; }
        public int ClientId { get; set; }
        public int ConferenceId { get; set; }
        public int ConferenceDay { get; set; }
        public int NumberOfSeats { get; set; }
        public ReservationState State { get; set; }
        public double TotalPrice { get; set; }
        public double PayedAmount { get; set; }
        public DateTime CreationDate { get; set; }
        public List<ParticipantModel> Participants { get; set; }
        public double Discount { get; set; }
    }
}
