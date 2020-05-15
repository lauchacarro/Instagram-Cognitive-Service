using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Blazored.LocalStorage;

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace InstagramComputerVision.Services
{
    public class ComputerVisionService : IComputerVisionService
    {
        private readonly ILocalStorageService _localStorageService;

        public ComputerVisionService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;

        }

        public async Task<ComputerVisionClient> GetClient()
        {
            string computerVisionKey = await _localStorageService.GetItemAsync<string>(Constant.COMPUTERVISIONKEY);
            string computerVisionEndpoint = await _localStorageService.GetItemAsync<string>(Constant.COMPUTERVISIONENDPOINT);

            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(computerVisionKey))
            { Endpoint = computerVisionEndpoint };

            return client;
        }

        public async Task<ImageAnalysis> AnalyzeImageUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).Cast<VisualFeatureTypes>().ToList();
            List<Details> details = Enum.GetValues(typeof(Details)).Cast<Details>().ToList();

            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features, details);
            return results;
        }

        public async Task<OcrResult> OcrUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            OcrResult remoteOcrResult = await client.RecognizePrintedTextAsync(true, imageUrl);
            return remoteOcrResult;
        }

        public async Task<Stream> GenerateThumbnailUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            Stream thumbnailUrl = await client.GenerateThumbnailAsync(60, 60, imageUrl, true);
            return thumbnailUrl;
        }

        public async Task<BatchReadFileHeaders> BatchReadFileUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            BatchReadFileHeaders textHeaders = await client.BatchReadFileAsync(imageUrl);
            return textHeaders;
        }

        public async Task<DetectResult> DetectObjectsUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            DetectResult detectObjectAnalysis = await client.DetectObjectsAsync(imageUrl);
            return detectObjectAnalysis;
        }

        public async Task<DomainModelResults> DetectDomainSpecificUrl(string imageUrl)
        {
            ComputerVisionClient client = await GetClient();

            DomainModelResults resultsUrl = await client.AnalyzeImageByDomainAsync("landmarks", imageUrl);
            return resultsUrl;
        }
    }
}
