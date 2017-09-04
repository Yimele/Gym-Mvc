using MVCWithWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCWithWebService.DAL
{
    public class GymInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<GymContext>
    {
        protected override void Seed(GymContext context)
        {
            var customers = new List<Customer>
            {
            new Customer{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            };

            customers.ForEach(s => context.Customers.Add(s));
            context.SaveChanges();
            var tracks = new List<Track>
            {
            new Track{TrackID=1050,Title="Zumba"},
            };
            tracks.ForEach(s => context.Tracks.Add(s));
            context.SaveChanges();
            var enrollments = new List<Enrollment>
            {
            new Enrollment{CustomerID=1,TrackID=1050},
            };
            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}