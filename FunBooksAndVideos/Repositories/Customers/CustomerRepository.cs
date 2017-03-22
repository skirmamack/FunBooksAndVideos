using System;
using System.Collections.Generic;
using FunBooksAndVideos.Model.Customers;
using FunBooksAndVideos.Model.Products.Memberships;

namespace FunBooksAndVideos.Repositories.Customers
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public override IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public void ActivateMembership(int customerId, Membership membership)
        {
            throw new NotImplementedException();
        }
    }
}
