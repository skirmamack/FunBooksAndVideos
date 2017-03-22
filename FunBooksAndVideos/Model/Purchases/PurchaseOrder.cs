using System.Collections.Generic;
using FunBooksAndVideos.Model.Products;

namespace FunBooksAndVideos.Model.Purchases
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<BaseProduct> Items { get; set; }
    }
}
