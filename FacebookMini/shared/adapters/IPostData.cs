namespace FacebookMini.shared.adapters
{
    public interface IPostData
    {
        string Id { get; }
        string OwnerName { get; }
        string OwnerPictureUrl { get; }
        string CreatedTimeText { get; }
        string CaptionText { get; }
        int LikesCount { get; set; }
        int CommentsCount { get; }
        string LikesText { get; }
        string CommentsText { get; }
    }
}
