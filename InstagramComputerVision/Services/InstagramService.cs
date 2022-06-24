using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;

using InstagramComputerVision.Models;
using InstagramComputerVision.Options;

using Microsoft.Extensions.Options;

namespace InstagramComputerVision.Services
{
    public class InstagramService : IInstagramService
    {
        private readonly IInstaApi _instaApi;
        public InstagramService(IOptions<InstagramOptions> options)
        {

            if (string.IsNullOrWhiteSpace(options.Value.Username))
            {
                throw new ArgumentException("El Username esta vacio");
            }

            if (string.IsNullOrWhiteSpace(options.Value.Password))
            {
                throw new ArgumentException("El Password esta vacio");
            }

            UserSessionData userSession = new UserSessionData
            {
                UserName = options.Value.Username,
                Password = options.Value.Password
            };

            _instaApi = InstaApiBuilder.CreateBuilder()
                   .SetUser(userSession)
                   .Build();
        }

        public async Task<IEnumerable<InstaPost>> GetInstaPosts(string key)
        {
            if (!_instaApi.IsUserAuthenticated)
            {
                await _instaApi.LoginAsync();
            }

            List<InstaMedia> medias;
            if (key.StartsWith("@"))
            {
                var userMedia = await _instaApi.UserProcessor.GetUserMediaAsync(key.Substring(1), PaginationParameters.MaxPagesToLoad(5));
                medias = userMedia.Value;
            }
            else
            {
                var tagFeed = await _instaApi.FeedProcessor.GetTagFeedAsync(key, PaginationParameters.MaxPagesToLoad(1));
                medias = tagFeed.Value.Medias;
            }

            if (medias is null)
            {
                return Enumerable.Empty<InstaPost>();
            }
            else
            {
                return medias.Select(x => new InstaPost
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
}
