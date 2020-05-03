using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;

using InstagramComputerVision.Models;
using InstagramComputerVision.Options;

using Microsoft.Extensions.Options;

namespace InstagramComputerVision.Services
{
    public class InstagramService
    {
        private readonly IInstaApi _instaApi;
        public InstagramService(IOptions<InstagramOptions> options)
        {
            UserSessionData userSession = new UserSessionData
            {
                UserName = options.Value.Username,
                Password = options.Value.Password
            };

            _instaApi = InstaApiBuilder.CreateBuilder()
                   .SetUser(userSession)
                   .Build();
        }

        public async Task<IEnumerable<InstaPost>> GetInstaMedias(string tag)
        {
            if (!_instaApi.IsUserAuthenticated)
            {
                await _instaApi.LoginAsync();
            }

            var tagFeed = await _instaApi.FeedProcessor.GetTagFeedAsync(tag, PaginationParameters.MaxPagesToLoad(1));

            return tagFeed.Value.Medias.Select(x => new InstaPost
            {
                Username = x.User.UserName,
                Caption = x.Caption?.Text ?? string.Empty,
                HasLiked = x.HasLiked,
                LikesCount = x.LikesCount,
                UserPicture = x.User.ProfilePicture,
                ImageUrl = x.Images.FirstOrDefault()?.Uri

            });

        }
    }
}
