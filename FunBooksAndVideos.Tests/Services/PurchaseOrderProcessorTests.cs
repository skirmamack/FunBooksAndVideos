using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using FunBooksAndVideos.Services;
using FunBooksAndVideos.Services.Documents;
using FunBooksAndVideos.Model.Purchases;
using FunBooksAndVideos.Model.Products;
using FunBooksAndVideos.Model.Products.Memberships;
using FunBooksAndVideos.Repositories.Customers;
using FunBooksAndVideos.Tests.Stubs;


namespace FunBooksAndVideos.Tests
{
    [TestFixture]
    public class PurchaseOrderProcessorTests
    {
        private PurchaseOrderProcessor _processor;
        private Mock<ICustomerRepository> _customerRepository;
        private ShippingSlipGeneratorStub _shippingSlipGenerator;

        private const int TestCustomerId = 333;
        private readonly Membership _bookClubMembership = new BookClubMembership();
        private readonly Membership _videoClubMembership = new VideoClubMembership();
        private readonly Membership _premiumMembership = new PremiumMembership();

        private readonly Product _physicalProduct1 = new Product
        {
            Title = "Some book 1"
        };

        private readonly Product _physicalProduct2 = new Product
        {
            Title = "Some book 2"
        };

        private string ShippingSlipDocumentUrl = "Some Url";

        private static PurchaseOrder GeneratePurchaseOrder(params BaseProduct[] products)
        {
            var items = new List<BaseProduct>(products);
            return new PurchaseOrder{
                CustomerId = TestCustomerId,
                Items = items
            };
        }

        [SetUp]
        public void SetUp()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _shippingSlipGenerator = new ShippingSlipGeneratorStub(ShippingSlipDocumentUrl);

            _processor = new PurchaseOrderProcessor(_customerRepository.Object, _shippingSlipGenerator);
        }

        [Test]
        public void should_ActivateBookClubMembership_when_BookClubMembershipPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1, _bookClubMembership, _physicalProduct2);

            _processor.Process(purchaseOrder);

            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _bookClubMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _videoClubMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _premiumMembership), Times.Never);    
        }

        [Test]
        public void should_ActivateVideoClubMembership_when_VideoClubMembershipPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1, _videoClubMembership, _physicalProduct2);

            _processor.Process(purchaseOrder);

            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _videoClubMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _bookClubMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _premiumMembership), Times.Never);    
        }

        [Test]
        public void should_ActivatePremiumMembership_when_PremiumClubMembershipPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1, _premiumMembership, _physicalProduct1);

            _processor.Process(purchaseOrder);

            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _premiumMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _bookClubMembership), Times.Never);    
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _videoClubMembership), Times.Never);            
        }

        [Test]
        public void should_ActivateMixedClubMemberships_when_MixedMembershipsPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1, _bookClubMembership, _videoClubMembership);

            _processor.Process(purchaseOrder);

            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _bookClubMembership));
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _videoClubMembership));            
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _premiumMembership), Times.Never);
        }

        [Test]
        public void shouldNot_ActivateMemberships_when_NoMembershipPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1);

            _processor.Process(purchaseOrder);

            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _premiumMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _bookClubMembership), Times.Never);
            _customerRepository.Verify(customers => customers.ActivateMembership(TestCustomerId, _videoClubMembership), Times.Never);
        }

        [Test]
        public void should_GenerateShippingSlip_when_PhysicalProductPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_physicalProduct1);

            var result = _processor.Process(purchaseOrder);

            Assert.That(result.ShippingSlipUrl, Is.EqualTo(ShippingSlipDocumentUrl));
        }

        [Test]
        public void shouldNot_GenerateShippingSlip_when_NoPhysicalProductPurchased()
        {
            var purchaseOrder = GeneratePurchaseOrder(_bookClubMembership);

            var result = _processor.Process(purchaseOrder);

            Assert.That(result.ShippingSlipUrl, Is.Null);
        }
    }
}
