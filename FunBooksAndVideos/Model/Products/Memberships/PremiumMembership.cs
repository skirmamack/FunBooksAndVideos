namespace FunBooksAndVideos.Model.Products.Memberships
{
    public class PremiumMembership : Membership
    {
        public override string Title
        {
            get
            {
                return base.Title ?? Constants.PremiumClubMembership;
            }
        }
    }
}
