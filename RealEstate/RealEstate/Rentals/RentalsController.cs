using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using RealEstate.App_Start.DataContext;

namespace RealEstate.Rentals
{
    public class RentalsController : Controller
    {
        public readonly  RealEstateContext context = new RealEstateContext();
        // GET: Rentals
        public ActionResult Index(RentalsFilter filters)
        {
            if (filters.MinimumRooms == null)
            {
                filters.MinimumRooms = 0;
                filters.PriceLimit = 1000000;
            }
            var rentals = FilterRentals(filters);
            var model = new RentalsList
            {
                Rentals = rentals,
                Filters = filters
            };

            return View(model);
        }

        private IEnumerable<Rental> FilterRentals(RentalsFilter filters)
        {
            IQueryable<Rental> rentals = context.Rentals.AsQueryable()
                .OrderBy(r => r.Price);

            if (filters.MinimumRooms.HasValue)
            {
                rentals = rentals
                    .Where(r => r.RoomCount >= filters.MinimumRooms);
            }

            if (filters.PriceLimit >=0)
            {
                var query = Query<Rental>.LTE(r => r.Price, filters.PriceLimit);
                rentals = rentals.Where(r => query.Inject());
            }

            return rentals;
        }

        public ActionResult PostRental() 
        {

            return View();
        }

        [HttpPost]
        public ActionResult Post(Postrental postRental)
        {
            var rental = new Rental(postRental);
            context.Rentals.Insert(rental);
            return RedirectToAction("Index");
        }

        public ActionResult AdjustPrice(string id)
        {
            //sending id by changind it object id as mongodb stores it as objectid
            var rental = GetRental(id);
            return View(rental);
        }

        private Rental GetRental(string id)
        {
            var rental = context.Rentals.FindOneById(new BsonObjectId(id));
            return rental;
        }

        [HttpPost]
        public ActionResult AdjustPrice(string id, AdjustPrice adjustPrice)
        {
            var rental = GetRental(id);
            rental.AdjustPrice(adjustPrice);
            context.Rentals.Save(rental);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            context.Rentals.Remove(Query.EQ("_id", new ObjectId(id)));
            return RedirectToAction("Index");
        }

        public string PriceDistribution()
        {
          return  new QueryPriceDistribution()
                .Run(context.Rentals).ToJson();


        }

        public ActionResult AttachImage(string id)
        {
            var rental = GetRental(id);
            return View(rental);
        }

        [HttpPost]
        public ActionResult Attachimage(string id, HttpPostedFileBase file)
        {
            var rental = GetRental(id);
            if (rental.HasImage())
            {
                DeleteImage(rental);
            }
            StoreImage(file, rental);
            // context.Rentals.Save(rental);
            return RedirectToAction("Index");
        }

        private void DeleteImage(Rental rental)
        {
            context.DataBase.GridFS.DeleteById(new BsonObjectId(rental.imageid));
            rental.imageid = null;
            context.Rentals.Save(rental);
        }

        private void StoreImage(HttpPostedFileBase file, Rental rental)
        {
            var Imageid = ObjectId.GenerateNewId();
            rental.imageid = Imageid.ToString();
            context.Rentals.Save(rental);

            var options = new MongoGridFSCreateOptions
            {
                Id = Imageid,
                ContentType = file.ContentType
            };
            context.DataBase.GridFS.Upload(file.InputStream, file.FileName);
        }

        public ActionResult GetImage(string id)
        {

            var image = context.DataBase.GridFS
                .FindOneById(new ObjectId(id));


            if (image == null)
            {
                return HttpNotFound();
            }
            return File(image.OpenRead(), image.ContentType);
        }
    }
}