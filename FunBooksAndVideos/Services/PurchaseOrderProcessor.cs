using System.Linq;
using FunBooksAndVideos.Model.Purchases;
using FunBooksAndVideos.Services.Documents;
using FunBooksAndVideos.Model.Products.Memberships;
using FunBooksAndVideos.Repositories.Customers;
using FunBooksAndVideos.Model.Products;

namespace FunBooksAndVideos.Services
{
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IShippingSlipGenerator _shippingSlipGenerator;

        public PurchaseOrderProcessor() : this(new CustomerRepository(), new ShippingSlipGenerator())
        {
        }

        public PurchaseOrderProcessor(ICustomerRepository customerRepository, IShippingSlipGenerator shippingSlipGenerator)
        {
            _customerRepository = customerRepository;
            _shippingSlipGenerator = shippingSlipGenerator;
        }

        public PurchaseOrderProcessResult Process(PurchaseOrder purchaseOrder)
        {
            var memberships = purchaseOrder.Items
                .Where(item => item is Membership)
                .Cast<Membership>();

            string shippingSlipUrl;

            foreach (var membership in memberships)
            {
                _customerRepository.ActivateMembership(purchaseOrder.CustomerId, membership);
            }

            if (purchaseOrder.Items.Any(item => item is Product))
            {
                shippingSlipUrl = _shippingSlipGenerator.Generate(purchaseOrder.Items);
            }
            else
            {
                shippingSlipUrl = null;
            }

            return new PurchaseOrderProcessResult
            {
                ShippingSlipUrl = shippingSlipUrl
            };
        }
    }
}
