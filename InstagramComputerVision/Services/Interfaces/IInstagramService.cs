using System.Collections.Generic;
using System.Threading.Tasks;

using InstagramComputerVision.Models;

namespace InstagramComputerVision.Services
{
    public interface IInstagramService
    {
        Task<IEnumerable<InstaPost>> GetInstaPosts(string key);
    }
}