using FunBooksAndVideos.Model.Purchases;

namespace FunBooksAndVideos.Services
{
    interface IPurchaseOrderProcessor
    {
        PurchaseOrderProcessResult Process(PurchaseOrder purchaseOrder);
    }
}
