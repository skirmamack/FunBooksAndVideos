namespace FunBooksAndVideos.Services.Documents
{
    public interface IShippingSlipGenerator
    {
        string Generate(System.Collections.Generic.IEnumerable<FunBooksAndVideos.Model.Products.BaseProduct> products);
    }
}
