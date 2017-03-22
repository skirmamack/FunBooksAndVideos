using System;
using System.Collections.Generic;
using FunBooksAndVideos.Model.Products.Memberships;

namespace FunBooksAndVideos.Model.Customers
{
    public class Customer
    {
        public int CustomerId { get; set; }        

        public IEnumerable<Membership> Memberships { get; set; }

        // other Customer properties
    }
}
