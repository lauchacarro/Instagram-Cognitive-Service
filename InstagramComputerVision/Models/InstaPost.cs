namespace InstagramComputerVision.Models
{
    public class InstaPost
    {
        public string Username { get; set; }
        public string UserPicture { get; set; }
        public string ImageUrl { get; set; }
        public bool HasLiked { get; set; }
        public int LikesCount { get; set; }
        public string Caption { get; set; }
    }
}
