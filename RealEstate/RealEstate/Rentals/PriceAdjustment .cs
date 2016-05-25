using System.ComponentModel.Design.Serialization;

namespace RealEstate.Rentals
{
    public class PriceAdjustment 
    {
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string Reason { get; set; }

        public PriceAdjustment()
        {
        }

        public PriceAdjustment(AdjustPrice adjustPrice, decimal oldPrice)
        {
            OldPrice = oldPrice;
            NewPrice = adjustPrice.newPrice;
            Reason = adjustPrice.Reasong;
        }

        public object Describe()
        {
            return string.Format("{0}->{1}:{2}", OldPrice, NewPrice, Reason);
        }
    }
}