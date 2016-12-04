using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConferenceManagementSystemGenerator.Models;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class ConferenceProvider
    {
        WorkshopProvider _workshopProvider { get; set; }
        int LastConferenceId { get; set; }
        Random randomGenerator;

        public ConferenceProvider(int lastConferenceId, int LastWorkshopId)
        {
            this.LastConferenceId = lastConferenceId;
            _workshopProvider = new WorkshopProvider(LastWorkshopId);
            randomGenerator = new Random();
        }

        public List<ConferenceModel> InitConferencesList(int ConferencesAmount, DateTime StartDate, AddressProvider _addressProvider)
        {
            var result = new List<ConferenceModel>();
            for (int i = 0; i<ConferencesAmount; i++)
            {
                var newConference = new ConferenceModel();
                // Id konferencji
                newConference.ConferenceId = ++LastConferenceId;
                // Nazwa konferencji
                newConference.ConferenceName = Guid.NewGuid().ToString();
                // Ramy czasowe konferencji
                newConference.ConferenceLength = randomGenerator.Next(1, 4);
                newConference.StartDate = new DateTime(StartDate.Year, StartDate.Month, randomGenerator.Next(1, StartDate.Month == 2 ? 29 : new List<int>() { 4, 6, 9, 11 }.Any(x => x == StartDate.Month) ? 31 : 32), randomGenerator.Next(8,11), 0,0);
                newConference.FinishDate = newConference.StartDate.AddDays(newConference.ConferenceLength-1).AddHours(randomGenerator.Next(8, 13));
                if (i % 2 == 1 && i != 1)
                    StartDate = StartDate.AddMonths(1);
                if (i % 24 == 1 && i != 1)
                    StartDate = StartDate.AddYears(1);
                // Adres konferencji
                newConference.Address = _addressProvider.GenerateAddress();
                // Maksymalna ilość uczestników
                newConference.MaxParticipantsNumber = randomGenerator.Next(30, 41)*10;
                // Podstawowa stawka za jeden dzień konferencji w '$'
                newConference.BasePrice = randomGenerator.Next(40, 101);
                // Stan konferencji
                if (newConference.FinishDate < DateTime.Now)
                    newConference.State = Consts.ConferenceState.Completed;
                else
                    newConference.State = Consts.ConferenceState.Planned;
                // Inicjalizacja kolejnych dni konferencji
                var conferenceDays = new List<ConferenceDayModel>();
                for (int c = 0; c < newConference.ConferenceLength; c++)
                    conferenceDays.Add(InitializeConferenceDay(newConference.StartDate.AddDays(c).Date, newConference.StartDate.Hour, newConference.FinishDate.Hour));
                newConference.Days = conferenceDays;
                result.Add(newConference);
            }
            return result;
        }
        
        public ConferenceDayModel InitializeConferenceDay(DateTime day, int conferenceStartHour, int conferenceFinishHour)
        {
            var result = new ConferenceDayModel();
            result.ParticipantsNumber = randomGenerator.Next(100, 301);
            int workshopsAmount = randomGenerator.Next(3, 6);
            for (int i = 0; i < workshopsAmount; i++)
                result.Workshops.Add(_workshopProvider.InitializeWorkshop(randomGenerator, LastConferenceId, day, conferenceStartHour, conferenceFinishHour));
            return result;
        }
    }
}
