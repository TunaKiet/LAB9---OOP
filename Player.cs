using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LAP9_OOP
{
    [DataContract]
    internal class Player
    {
        [DataMember] public string Username { get; set; }
        [DataMember] public decimal Reward { get; set; }
        [DataMember] public List<Product> Products { get; set; }

        public Player(string username)
        {
            Username = username;
            Reward = 100m;
            Products = new List<Product>();
        }

        public void EarnReward(decimal point)
        {
            Reward += point;
            Console.WriteLine($"{Username} đã nhận được {point} điểm!");
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
            Console.WriteLine($"{product.GetType().Name} đã được thêm vào trang trại của {Username}.");
        }
        public void SaveData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Player), new Type[] { typeof(Product), typeof(Wheat), typeof(Tomato), typeof(Sunflower) });
                serializer.WriteObject(fs, this);
                Console.WriteLine("Dữ liệu đã được lưu.");
            }
        }


        public static Player LoadData(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(Player), new Type[] { typeof(Product), typeof(Wheat), typeof(Tomato), typeof(Sunflower) });
                return (Player)serializer.ReadObject(fs);
            }
        }
    }
}
