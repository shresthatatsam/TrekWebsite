using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserRoles.Data;
using UserRoles.Models;
using static System.Net.WebRequestMethods;

namespace UserRoles.Services.Seeder
{
    public class DbSeeder
    {
        public static void SeedAboutUs(AppDbContext context)
        {
            if (!context.CarousalImages.Any())
            {
                var carousals = new List<CarousalImage>
                {
                new CarousalImage
                {
                    Id = Guid.Parse("E1EED0D6-D1CA-44A7-B046-CB6E72EF6455"),
                    ImageUrl = "https://tse2.mm.bing.net/th/id/OIP.3ufb8UJ0D-JinCkNSCdcfAHaD5?rs=1&pid=ImgDetMain&o=7&rm=3",
                    Caption = "Explore Annapurna",
                    SubCaption = "Join guided adventures to Nepal's highest peaks",
                    IsActive = true,
                    carousalEnum=CarousalEnum.Home
                },
                new CarousalImage
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "https://tse1.mm.bing.net/th/id/OIP.qvosaBZ6VPZzt64FAEaxzAHaFM?rs=1&pid=ImgDetMain&o=7&rm=3",
                    Caption = "Explore the Himalayas",
                    SubCaption = "Join guided adventures to Nepal's highest peaks",
                    IsActive = true,
                    carousalEnum=CarousalEnum.Home
                },
                new CarousalImage
                {
                    Id = Guid.NewGuid(),
                    ImageUrl = "https://th.bing.com/th/id/OIP.dZqzfLrxbZAEydDfiMSibAHaE7?o=7rm=3&rs=1&pid=ImgDetMain&o=7&rm=3",
                    Caption = "Tranquil Trails",
                    SubCaption = "Discover peaceful lakeside treks and hidden valleys",
                    IsActive = true,
                    carousalEnum=CarousalEnum.Home
                }
            };

                context.CarousalImages.AddRange(carousals);
                context.SaveChanges();
            }

            if (!context.AboutUs.Any())
            {
                context.AboutUs.Add(new AboutUs
                {
                    Title = "Travel made easy",
                    Story = "we are passionate about exploring the majestic landscapes of Nepal and sharing that magic with fellow travelers. Founded by a team of seasoned trekkers and local guides, our mission is to make trekking in Nepal accessible, safe, and unforgettable for adventurers of all levels.\r\n\r\nFrom the towering peaks of the Himalayas to serene mountain villages, we craft personalized trekking experiences that connect you deeply with nature and culture. We believe trekking is more than just a journey — it’s a chance to challenge yourself, find peace, and create lifelong memories.",
                    Mission = "abc",
                    IsActive = true,
                    ImageUrl = "https://media.istockphoto.com/photos/about-us-picture-id816887384?k=20&m=816887384&s=612x612&w=0&h=P3p2ciEwnLwDAK-7JoWZDTZJl9uRrJaGdLhuVbmMkJg="
                });

                context.SaveChanges();
            }

            if (!context.Deals.Any())
            {
                var commonFeatures = new List<string> { "full board", "round trip", "sharing room", "meals", "lunch", "breakfast" };
                string serializedFeatures = JsonConvert.SerializeObject(commonFeatures);

                context.Deals.AddRange(
                    new Deals
                    {
                        Id = Guid.Parse("696E40CD-E1C4-4A33-93DD-F34F644731CF"),
                        ImageUrl = "https://as1.ftcdn.net/v2/jpg/02/77/65/52/1000_F_277655261_qqDRmFdGjYXh9J2wdGqBBVNuDKmqsOfY.jpg",
                        Caption = "Everest Base Camp",
                        SubCaption = "Journey to the top of the world",
                        Details = "Explore the iconic Everest Base Camp with experienced guides.",
                        Header = "EBC Trek",
                        Amount = 120000.00m,
                        Features = serializedFeatures,
                        Isactive = true
                    },
                    new Deals
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://getawaytrekking.com.au/wp-content/uploads/2020/11/DSC_0803-1626x1080-1.jpg",
                        Caption = "Annapurna Circuit",
                        SubCaption = "Adventure through diverse landscapes",
                        Details = "Trek through lush forests, high mountain passes, and traditional villages.",
                        Header = "Annapurna Trek",
                        Amount = 95000.00m,
                        Features = serializedFeatures,
                        Isactive = true
                    },
                    new Deals
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl = "https://tse4.mm.bing.net/th/id/OIP.HvlrtLO0FF_7MMEGl7Yx3QHaDG?rs=1&pid=ImgDetMain&o=7&rm=3",
                        Caption = "Langtang Valley",
                        SubCaption = "A hidden Himalayan gem",
                        Details = "Perfect for beginners and nature lovers, close to Kathmandu.",
                        Header = "Langtang Trek",
                        Amount = 75000.00m,
                        Features = serializedFeatures,
                        Isactive = true
                    }
                );

                context.SaveChanges();
            }


        }
    }
}
