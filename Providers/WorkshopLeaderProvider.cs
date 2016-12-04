using System;
using System.Collections.Generic;
using SqlConferenceManagementSystemGenerator.Models;

namespace SqlConferenceManagementSystemGenerator.Providers
{
    public class WorkshopLeaderProvider
    {
        List<string> FirstNames { get; set;}
        List<string> LastNames { get; set; }
        List<string> CompanyNames { get; set; }
        List<string> Emails;
        int LastLeaderId { get; set; }
        Random randomGenerator;

        public WorkshopLeaderProvider(List<string> firstNames, List<string> lastNames, List<string> companyNames, int LastLeaderId, List<string> emails)
        {
            FirstNames = firstNames;
            LastNames = lastNames;
            CompanyNames = companyNames;
            this.LastLeaderId = LastLeaderId;
            randomGenerator = new Random();
            Emails = emails;
        }

        public WorkshopLeaderModel CreateLeader()
        {
            return new WorkshopLeaderModel()
            {
                LeaderId = ++LastLeaderId,
                FirstName = FirstNames[randomGenerator.Next(0, FirstNames.Count)],
                LastName = LastNames[randomGenerator.Next(0, LastNames.Count)],
                Phone = randomGenerator.Next(100000000, 999999999).ToString(),
                CompanyName = randomGenerator.Next() % 3 != 0 ? CompanyNames[randomGenerator.Next(0, CompanyNames.Count)] : "",
                Email = Emails[randomGenerator.Next(0, Emails.Count)]
            };
        }
    }
}
