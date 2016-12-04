using SqlConferenceManagementSystemGenerator.Models;
using SqlConferenceManagementSystemGenerator.Consts;
using System.Collections.Generic;
using System;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class WorkshopProvider
    {
        public int LastWOrkshopId { get; set; }

        public WorkshopProvider(int lastWorkshopId)
        {
            LastWOrkshopId = lastWorkshopId;
        }

        public WorkshopModel InitializeWorkshop(Random randomGenerator, int conferenceId, DateTime day, int conferenceStartHour, int conferenceFinishHour)
        {
            var result = new WorkshopModel();
            result.BasePrice = randomGenerator.Next(20, 51);
            result.ConferenceId = conferenceId;
            result.StartTime = day.Date;
            result.StartTime = result.StartTime.AddHours(randomGenerator.Next(conferenceStartHour, conferenceFinishHour-2));
            result.EndTime = day.Date;
            result.EndTime = result.EndTime.AddHours(randomGenerator.Next(result.StartTime.Hour+1, conferenceFinishHour+1));
            result.WorkshopId = ++LastWOrkshopId;
            result.MaxParticipantNumber = randomGenerator.Next(20, 51);
            result.WorkshopName = Guid.NewGuid().ToString();
            return result;
        }
    }
}
