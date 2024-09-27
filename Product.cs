using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LAP9_OOP
{
    [DataContract]
    public abstract class Product
    {
        [DataMember] public decimal Cost { get; set; }
        [DataMember] public decimal Value { get; set; }
        [DataMember] public DateTime Start { get; set; }
        [DataMember] public TimeSpan Duration { get; set; }
        [DataMember] public decimal FertilizerCost = 2.0m;
        [DataMember] public decimal WaterCost = 1.0m;

        public abstract void Seed();
        public abstract decimal Harvest();
        public abstract void Feed(int numFertilizer);
        public abstract void ProvWater(int numWater);
    }
}
