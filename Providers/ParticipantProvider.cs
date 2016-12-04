using System;
using System.Linq;
using System.Collections.Generic;
using SqlConferenceManagementSystemGenerator.Models;
using SqlConferenceManagementSystemGenerator.Consts;
using System.Text;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class ParticipantProvider
    {
        int lastParticipantId;
        AddressProvider _addressProvider;
        List<string> firstNames;
        List<string> lastNames;
        List<string> emails;
        Random randomGenerator;

        public ParticipantProvider(int LastParticipantId, List<string> firstNames, List<string> lastNames, List<string> emails, AddressProvider iAddressProvider)
        {
            this.lastParticipantId = LastParticipantId;
            this.emails = emails;
            this.lastNames = lastNames;
            this.firstNames = firstNames;
            randomGenerator = new Random();
            _addressProvider = iAddressProvider;
        }

        public List<ParticipantModel> GenerateParticipants(ClientModel client, ConferenceReservationModel reservation)
        {
            if (client.ClientType == ClientType.IndividualClient)
            {
                var name = client.ClientName.Split(' ');
                return new List<ParticipantModel>()
                {
                    new ParticipantModel()
                    {
                        ClientId = client.ClientId,
                        ParticipantId = ++lastParticipantId,
                        FirstName = name[0],
                        LastName = name[1],
                        Address = client.ContactAddress,
                        PhoneNumber = client.Phone,
                        PESEL = client.PESEL,
                        Email = client.Email
                    }
                };
            }
            else
            {
                var result = new List<ParticipantModel>();
                for (int i = 0; i < reservation.NumberOfSeats; i++)
                    result.Add(GenerateRandomParticipant(client.ClientId));
                return result;
            }
        }

        private ParticipantModel GenerateRandomParticipant(int ClientId)
        {
            return new ParticipantModel()
            {
                ParticipantId = ++lastParticipantId,
                Address = _addressProvider.GenerateAddress(),
                PhoneNumber = randomGenerator.Next(100000000, 999999999).ToString(),
                PESEL = randomGenerator.Next(100000, 999999).ToString() + randomGenerator.Next(10000, 99999).ToString(),
                ClientId = ClientId,
                FirstName = firstNames[randomGenerator.Next(0, firstNames.Count)],
                LastName = lastNames[randomGenerator.Next(0, lastNames.Count)],
                Email = emails[randomGenerator.Next(0, emails.Count)]
            };
        }

        public List<ParticipantModel> AttachParticipantsToWorkshop(WorkshopModel workshop, List<ParticipantModel> ClientParticipants, int ParticipantsNumber, ConferenceDayModel conference)
        {
            ClientParticipants = ClientParticipants.Where(x => !(conference.Workshops.Any(w => w.Reservations.Any(r => r.Participants.Any(p => p.ParticipantId == x.ParticipantId)) && ((w.StartTime > workshop.StartTime && w.StartTime < workshop.EndTime) || w.EndTime > workshop.StartTime && w.EndTime < workshop.EndTime)))).ToList();
            var result = new List<ParticipantModel>();
            for (int i=0; i<ParticipantsNumber; i++)
            {
                if (ClientParticipants.Count == 0)
                    return result;
                var participant = ClientParticipants[randomGenerator.Next(0, ClientParticipants.Count)];
                result.Add(participant);
                ClientParticipants.Remove(participant);
            }
            return result;
        }
    }
}
