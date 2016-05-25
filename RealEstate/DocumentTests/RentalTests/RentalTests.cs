using MongoDB.Bson;
using NUnit.Framework;
using RealEstate.Rentals;

namespace DocumentTests.RentalTests
{
    [TestFixture]
    public class RentalTests : AssertionHelper
    {
        [Test]
        public void ToDocument_RentalPrice_DoubleRepresentation()
        {
            var rental = new Rental();
            rental.Price = 1;
            var document = rental.ToBsonDocument();
            Expect(document["Price"].BsonType, Is.EqualTo(BsonType.Double));
        }

        [Test]
        public void ToDocument_RentalID_storedObjectId()
        {
            var rental = new Rental();
            rental.id = ObjectId.GenerateNewId().ToString();
            var document = rental.ToBsonDocument();
            Expect(document["_id"].BsonType, Is.EqualTo(BsonType.ObjectId));
        }
    }
}