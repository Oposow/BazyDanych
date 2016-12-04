using System;
using System.Linq;
using System.Collections.Generic;
using SqlConferenceManagementSystemGenerator.Models;
using SqlConferenceManagementSystemGenerator.Consts;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class ConferenceReservationsProvider
    {
        int lastConferenceReservationId;
        Random randomGenerator;
        List<ClientModel> Clients;
        PriceStages Prices;
        ParticipantProvider _participantProvider;

        public ConferenceReservationsProvider(int LastConferenceReservationId, List<ClientModel> clients, ParticipantProvider iParticipantProvider)
        {
            lastConferenceReservationId = LastConferenceReservationId;
            Clients = clients;
            randomGenerator = new Random();
            Prices = new PriceStages();
            _participantProvider = iParticipantProvider;
        }

        public List<ConferenceReservationModel> CreateConferenceDayReservations(int participantsNumber, ConferenceModel conference, int conferenceDay)
        {
            int currentParticipantsNumber = 0;
            var result = new List<ConferenceReservationModel>();
            while (currentParticipantsNumber < participantsNumber)
            {
                var reservation = new ConferenceReservationModel()
                {
                    ReservationId = ++lastConferenceReservationId,
                    ConferenceId = conference.ConferenceId,
                    State = ReservationState.Payed,
                    ConferenceDay = conferenceDay
                };
                var clientsWithoutReservations = Clients.Where(x => !result.Any(y => x.ClientId == y.ClientId)).ToList();
                var client = clientsWithoutReservations[randomGenerator.Next(0, clientsWithoutReservations.Count())];
                reservation.ClientId = client.ClientId;
                var stage = Prices.Stages[randomGenerator.Next(0, Prices.Stages.Count)];
                reservation.NumberOfSeats = client.ClientType == ClientType.IndividualClient ? 1 : randomGenerator.Next(1, 16);
                if (reservation.NumberOfSeats + currentParticipantsNumber > participantsNumber)
                    reservation.NumberOfSeats = participantsNumber - currentParticipantsNumber;
                double discount = String.IsNullOrEmpty(client.StudentCardId) ? stage.Discount : stage.Discount + PriceStages.StudentDiscount;
                reservation.Discount = discount;
                reservation.TotalPrice = conference.BasePrice * (double)reservation.NumberOfSeats * (1-discount);
                reservation.PayedAmount = reservation.TotalPrice;
                reservation.CreationDate = conference.StartDate.Date.AddDays(-randomGenerator.Next(stage.DaysBeforePeriodEnds + 1, stage.DaysBeforePeriodStarts)).AddHours(randomGenerator.Next(0, 24)).AddMinutes(randomGenerator.Next(0, 60)).AddSeconds(randomGenerator.Next(0, 60));
                reservation.Participants = _participantProvider.GenerateParticipants(client, reservation);
                currentParticipantsNumber += reservation.NumberOfSeats;
                result.Add(reservation);
           }
            return result;
        }
    }
}
