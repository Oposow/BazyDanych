using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlConferenceManagementSystemGenerator.Models;
using SqlConferenceManagementSystemGenerator.Consts;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class WorkshopReservationsProvider
    {
        int lastWorshopReservationId;
        ParticipantProvider _participantProvider;
        List<ClientModel> clients;
        Random randomGenerator;
        PriceStages Prices;

        public WorkshopReservationsProvider(int LastWorkshopReservationId, ParticipantProvider iParticipantProvider, List<ClientModel> Clients)
        {
            lastWorshopReservationId = LastWorkshopReservationId;
            _participantProvider = iParticipantProvider;
            clients = Clients;
            randomGenerator = new Random();
            Prices = new PriceStages();
        }

        public List<WorkshopReservationModel> CreateWorkshopReservations(WorkshopModel workshop, ConferenceDayModel conference)
        {
            int i = 0;
            var ClientsInDay = clients.Where(c => conference.Reservations.Any(x => x.ClientId == c.ClientId)).ToList();
            var result = new List<WorkshopReservationModel>();
            int currentParticipantsNumber = 0;
            int participantsNumber = randomGenerator.Next(20, workshop.MaxParticipantNumber+1);
            while (currentParticipantsNumber < participantsNumber)
            {
                i++;
                var reservation = new WorkshopReservationModel()
                {
                    ReservationId = ++lastWorshopReservationId,
                    WorkshopId = workshop.WorkshopId,
                    State = ReservationState.Payed,
                };
                var client = ClientsInDay[randomGenerator.Next(0, ClientsInDay.Count)];
                var conferenceReservation = conference.Reservations.FirstOrDefault(x => x.ClientId == client.ClientId);
                reservation.ClientId = client.ClientId;
                var stage = Prices.Stages[randomGenerator.Next(0, Prices.Stages.Count)];
                reservation.NumberOfSeats = client.ClientType == ClientType.IndividualClient ? 1 : randomGenerator.Next(1, 10 < conferenceReservation.NumberOfSeats ? 11 : conferenceReservation.NumberOfSeats+1);
                if (reservation.NumberOfSeats + currentParticipantsNumber > participantsNumber)
                    reservation.NumberOfSeats = participantsNumber - currentParticipantsNumber;
                double discount = conferenceReservation.Discount;
                reservation.TotalPrice = workshop.BasePrice * (double)reservation.NumberOfSeats * (1 - discount);
                reservation.PayedAmount = reservation.TotalPrice;
                reservation.CreationDate = conferenceReservation.CreationDate;

                // Przypisywanie uczestników
                reservation.Participants = _participantProvider.AttachParticipantsToWorkshop(workshop, conferenceReservation.Participants, reservation.NumberOfSeats, conference);
                if (reservation.NumberOfSeats < reservation.Participants.Count)
                    reservation.NumberOfSeats = reservation.Participants.Count;
                currentParticipantsNumber += reservation.NumberOfSeats;
                ClientsInDay.Remove(client);
                if (reservation.NumberOfSeats != 0)
                    result.Add(reservation);
                // To na wypadek pętli nieskończonej
                if (i == 1000)
                    return result;
            }
            return result;
        }
    }
}
