namespace FunBooksAndVideos.Model.Products.Memberships
{
    public class BookClubMembership : Membership
    {
        public override string Title
        {
            get
            {
                return base.Title ?? Constants.BookClubMembership;
            }
        }
    }
}
