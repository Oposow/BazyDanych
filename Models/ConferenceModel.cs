using System;
using System.Collections.Generic;
using SqlConferenceManagementSystemGenerator.Consts;

namespace SqlConferenceManagementSystemGenerator.Models
{
    public class ConferenceModel
    {
        public int ConferenceId { get; set; }
        public string ConferenceName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int ConferenceLength { get; set; }
        public IEnumerable<ConferenceDayModel> Days { get; set; }
        public AddressModel Address { get; set; }
        public int MaxParticipantsNumber { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public ConferenceState State { get; set; }

        public ConferenceModel()
        {
            Days = new List<ConferenceDayModel>();
        }
    }
}
