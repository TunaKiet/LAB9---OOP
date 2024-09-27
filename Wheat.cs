using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LAP9_OOP
{
    [DataContract]
    public class Wheat : Product
    {
        [DataMember] public int NumFertilizer { get; set; }  // Đúng
        [DataMember] public int NumWater { get; set; }       // Đúng

        public Wheat()
        {
            Cost = 5m;
            Value = 13m;
            Duration = TimeSpan.FromSeconds(5);
        }

        public override void Seed()
        {
            Start = DateTime.Now;
            Console.WriteLine($"Lúa mì đã được gieo trồng vào {Start}. Thời gian chăm bón: {Duration} giây.");
        }

        public override decimal Harvest()
        {
            if (DateTime.Now < Start.Add(Duration))
                throw new InvalidOperationException("Lúa mì chưa đủ thời gian để thu hoạch.");



            decimal totalFertilizerCost = NumFertilizer * FertilizerCost;
            decimal totalWaterCost = NumWater * WaterCost;
            decimal totalCost = Cost + totalFertilizerCost + totalWaterCost;

            Console.WriteLine($"Bạn đã thu hoạch lúa mì với {NumFertilizer} lần bón phân và {NumWater} lần tưới nước.");
            Console.WriteLine($"Tổng chi phí: {totalCost}. Giá trị thu hoạch: {Value}.");

            decimal profit = Value - totalCost;
            Console.WriteLine($"Lãi thu được: {profit}.");
            return profit;
        }
        public override void Feed(int numFertilizer)
        {

            NumFertilizer += numFertilizer;
            Console.WriteLine($"Đã bón phân cho lúa mì {numFertilizer} lần.");
        }

        public override void ProvWater(int numWater)
        {

            NumWater += numWater;
            Console.WriteLine($"Đã tưới nước cho lúa mì {numWater} lần.");
        }
    }
}
