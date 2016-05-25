using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Rentals
{
    public class Rental
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Description { get; set; }
        public int RoomCount { get; set; }
        public List<string> Address = new List<string>();

        [BsonRepresentation(BsonType.Double)]
        public decimal Price { get; set; }
        public List<PriceAdjustment> Adjustments= new List<PriceAdjustment>();
        public string imageid { get; set; }

        public Rental()
        {
        }


        public Rental(Postrental postRental)
        {
            Description = postRental.Description;
            RoomCount = postRental.RoomCount;
            Price = postRental.Price;
            Address = (postRental.Address ?? string.Empty.Split('\n').ToList());
        }


        public void AdjustPrice(AdjustPrice adjustPrice)
        {
            var adjustment = new PriceAdjustment(adjustPrice, Price);
            Adjustments.Add(adjustment);
            Price = adjustPrice.newPrice;
            
        }

        public bool HasImage()
        {
            return string.IsNullOrWhiteSpace(imageid);
        }
    }
} 
