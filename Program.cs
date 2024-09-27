using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;
using LAP9_OOP;
using System;
using System.IO;

namespace OOP_LAB8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Player player = new Player("Peter");
            Console.WriteLine($"Chào mừng {player.Username} đến với HarvestFarm!");

            List<Product> plantedProducts = new List<Product>();
            Console.Write("Nhập tên file: ");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                Console.WriteLine("Tải dữ liệu trò chơi...");
                player = Player.LoadData(fileName);
            }
            else
            {
                player = new Player("Peter");
            }

            while (true)
            {
                Console.WriteLine($"Bạn có {player.Reward} điểm để gieo trồng.");
                Console.WriteLine("Chọn vật phẩm để gieo trồng:");
                Console.WriteLine("1. Lúa mì (5 điểm)");
                Console.WriteLine("2. Cà chua (3 điểm)");
                Console.WriteLine("3. Hoa hướng dương (30 điểm)");
                Console.WriteLine("4. Lưu dữ liệu trò chơi");
                Console.WriteLine("0. Thoát");

                string choice = Console.ReadLine();
                Product product = null;
                try
                {
                    switch (choice)
                    {
                        case "1":
                            if (player.Reward < 5)
                                throw new NotEnoughPointsException("Không đủ điểm để gieo trồng lúa mì.");
                            product = new Wheat();
                            break;
                        case "2":
                            if (player.Reward < 3)
                                throw new NotEnoughPointsException("Không đủ điểm để gieo trồng cà chua.");
                            product = new Tomato();
                            break;
                        case "3":
                            if (player.Reward < 30)
                                throw new NotEnoughPointsException("Không đủ điểm để gieo trồng hoa hướng dương.");
                            product = new Sunflower();
                            break;
                        case "4":
                            player.SaveData(fileName);
                            break;
                        case "0":
                            Console.WriteLine("Cảm ơn bạn đã chơi!");
                            return;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
                            continue;
                    }


                    if (product != null)
                    {
                        product.Seed();
                        player.AddProduct(product);
                        plantedProducts.Add(product);

                        Console.Write("Nhập số lần bón phân: ");
                        if (int.TryParse(Console.ReadLine(), out int fertilizer) && fertilizer >= 0)
                        {
                            product.Feed(fertilizer);
                        }

                        Console.Write("Nhập số lần tưới nước: ");
                        if (int.TryParse(Console.ReadLine(), out int water) && water >= 0)
                        {
                            product.ProvWater(water);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Không thể gieo trồng vật phẩm. Vật phẩm không hợp lệ.");
                    }
                }
                catch (NotEnoughPointsException e)
                {
                    Console.WriteLine($"Lỗi: {e.Message}");
                }

                System.Threading.Thread.Sleep(5000);



                foreach (Product plantedProduct in plantedProducts.ToList())
                {
                    try
                    {
                        decimal profit = plantedProduct.Harvest();
                        player.EarnReward(profit);
                        plantedProducts.Remove(plantedProduct);
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Lỗi: {ex.Message}");
                    }
                }

                Console.ReadLine();
            }
        }
    }
}