using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlConferenceManagementSystemGenerator.Consts
{
    public class PriceStages
    {
        public readonly List<PriceStage> Stages;
        public const double StudentDiscount = 0.3;

        public PriceStages()
        {
            Stages = new List<PriceStage>()
            {
                new PriceStage(0.3, 100, 31),
                new PriceStage(0.2, 30, 15),
                new PriceStage(0.1, 14,8),
                new PriceStage(0, 7,0)
            };
        }
    }

    public class PriceStage
    {
        public readonly double Discount;
        public readonly int DaysBeforePeriodStarts;
        public readonly int DaysBeforePeriodEnds;

        public PriceStage(double Discount, int DaysStart, int DaysEnd)
        {
            this.Discount = Discount;
            DaysBeforePeriodStarts = DaysStart;
            DaysBeforePeriodEnds = DaysEnd;
        }
    }
}
