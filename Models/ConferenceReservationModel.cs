using SqlConferenceManagementSystemGenerator.Consts;
using System;

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
        public decimal TotalPrice { get; set; }
        public decimal PayedAmount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
