using System;

namespace InstagramComputerVision.Helpers
{
    public static class CommonHelpers
    {
        public static string GetValidImageLink(string originalLink)
        {
            string corsAnywhere = "https://corsanywhere.herokuapp.com/";

            string instagramCDNDomain = "https://scontent-iad3-1.cdninstagram.com";

            Uri linkUri = new Uri(originalLink);

            var newlink = corsAnywhere + instagramCDNDomain + linkUri.PathAndQuery;

            return newlink;
        }
    }
}
