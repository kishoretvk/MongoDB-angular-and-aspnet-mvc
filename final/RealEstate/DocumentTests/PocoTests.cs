using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.AccessControl;
using MongoDB.Bson.Serialization.Attributes;

namespace DocumentTests
{
    public class PocoTests
    {
        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        public class Person
        {
            public string Fname { get; set; }
            public int age { get; set;}
            public List<string> Address = new List<string>();
            public Contact contct = new Contact();
            [BsonIgnore] //attributES THAT can be ignored
            public string IgnoreMe { get; set; }
            [BsonElement]
            private string imignored { get; set; }
            //exclude if null
            [BsonIgnoreIfNull] //ignore if null
            public string checknullatt { get; set; }
            // decimal to double
            [BsonRepresentation(BsonType.Double)]
            public decimal NetWorth { get; set; }
        }

        [Test]
        public void ignoreproperty()
        {
            var prsn = new Person();
            Console.WriteLine(prsn.ToJson());
        }

        public class Contact
        {
            public string Email { get; set; }
            
            public string Phone { get; set; }
        }

        [Test]
       public  void Automatic()
        {   
            var prsn = new Person
            {
                age = 27,
                Fname = "Kishore VK Terli",
            };
            prsn.NetWorth = 110/9;
            prsn.contct.Email = "vterli@my.bridgeport.edu";
            prsn.contct.Phone = "203-919-2865";
            prsn.Address.Add("1135 Iranistan ave");
            prsn.Address.Add("Bridgeport, CT");
            Console.WriteLine(prsn.ToJson());
        }


    }
}