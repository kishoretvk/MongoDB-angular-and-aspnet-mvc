using System.Collections.Generic;

namespace RealEstate.Rentals
{
    public class Postrental
    {
        public string Description { get; set; }
        public int RoomCount { get; set; }
        public decimal Price { get; set; }
        public List<string> Address = new List<string>();

    }
}