using System;
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
        public decimal TotalPrice { get; set; }
        public decimal PayedAmount { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
