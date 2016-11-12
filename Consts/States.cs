using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Consts
{
    public enum WorkshopState
    {
        Planned = 0,
        Cancelled = 1,
        Completed = 2 
    }

    public enum ConferenceState
    {
        Planned = 0,
        Cancelled = 1,
        Completed = 2
    }

    public enum ReservationState
    {
        NotPayed = 0,
        Payed = 1,
        Cancelled = 2
    }

}
