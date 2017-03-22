using System;
using FunBooksAndVideos.Model.Products.Memberships;

namespace FunBooksAndVideos.Repositories.Customers
{
    public interface ICustomerRepository
    {
        void ActivateMembership(int customerId, Membership membership);
    }
}
