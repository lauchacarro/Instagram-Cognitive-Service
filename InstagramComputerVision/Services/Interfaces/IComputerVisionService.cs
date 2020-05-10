using System.IO;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace InstagramComputerVision.Services
{
    public interface IComputerVisionService
    {
        Task<ImageAnalysis> AnalyzeImageUrl(string imageUrl);
        Task<Stream> GenerateThumbnailUrl(string imageUrl);
        Task<ComputerVisionClient> GetClient();
        Task<OcrResult> OcrUrl(string imageUrl);
    }
}