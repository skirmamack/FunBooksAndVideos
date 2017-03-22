using System;
using System.Collections.Generic;
using FunBooksAndVideos.Model.Products;

namespace FunBooksAndVideos.Services.Documents
{
    public class ShippingSlipGenerator : IShippingSlipGenerator
    {
        public string Generate(IEnumerable<BaseProduct> products)
        {
            throw new NotImplementedException();
        }
    }
}
