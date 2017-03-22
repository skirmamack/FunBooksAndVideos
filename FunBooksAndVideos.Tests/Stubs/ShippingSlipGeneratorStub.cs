using System;
using System.Collections.Generic;
using FunBooksAndVideos.Model.Products;
using FunBooksAndVideos.Services.Documents;

namespace FunBooksAndVideos.Tests.Stubs
{
    public class ShippingSlipGeneratorStub : IShippingSlipGenerator
    {
        private string _urlToGenerate;

        public ShippingSlipGeneratorStub(string urlToGenerate)
        {
            _urlToGenerate = urlToGenerate;
        }

        public string Generate(IEnumerable<BaseProduct> products)
        {
            return _urlToGenerate;
        }
    }
}
