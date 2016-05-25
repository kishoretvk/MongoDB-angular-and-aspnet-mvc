using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace DocumentTests
{
    public class BsonDocumentTest
    {
        public BsonDocumentTest()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();
            Console.WriteLine(document);
        }

        [Test]
        public void AddElement()
        {
            var person = new BsonDocument()
            {
                //explicit convertion of data type
                {"age", new BsonInt32(55) },
                //implict convertion
                { "IsAlive", true }
            };
            person.Add("FirstName", new BsonString("bill"));
            Console.WriteLine(person);
        }

        [Test]
        public void AddArrayExample()
        {
            var person = new BsonDocument();
            person.Add("address", new BsonArray(new[] {"1135  iranistan ave.", "bridgeport", "06605"}));
            Console.WriteLine(person);
        }

        [Test]
        public void EmbeddedDocument()
        {
            var Person = new BsonDocument
            {
                {
                "contact",
                new BsonDocument
                {
                    {"phone", "203-919-2865"},
                    {"Email", "vterli@my.bridgeport.edu"}
                }
                 }
                };
            Console.WriteLine(Person);
        }

        [Test]
        public void BsonConvertion()
        {
            var Person = new BsonDocument
            {
                {"age", 27}
            };

        Console.WriteLine(Person["age"]);
            //converting to integer type
            Console.WriteLine(Person["age"].AsInt32 + 10);
            //check type of data
            Console.WriteLine(Person["age"].IsInt32);
            Console.WriteLine(Person["age"].IsString);
        }

        [Test]
        public void ToBson()
        {
            var person = new BsonDocument { 
                {"FirstName", "venkata k kishore" }
                };
            var pfname = person.ToBson();
            Console.WriteLine(pfname);
            Console.WriteLine(BitConverter.ToString(pfname));
            //deserialize bson 
            Console.WriteLine(BsonSerializer.Deserialize<BsonDocument>(pfname));
        }
    }

}
