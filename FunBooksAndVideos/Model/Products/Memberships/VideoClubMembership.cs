namespace FunBooksAndVideos.Model.Products.Memberships
{
    public class VideoClubMembership : Membership
    {
        public override string Title
        {
            get
            {
                return base.Title ?? Constants.VideoClubMembership;
            }
        }
    }
}
