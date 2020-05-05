using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Blazored.LocalStorage;

using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

namespace InstagramComputerVision.Services
{
    public class ComputerVisionService
    {
        private readonly ILocalStorageService _localStorageService;

        public ComputerVisionService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;

        }

        public async Task<ImageAnalysis> AnalyzeImageUrl(string imageUrl)
        {
            string computerVisionKey = await _localStorageService.GetItemAsync<string>(Constant.COMPUTERVISIONKEY);
            string computerVisionEndpoint = await _localStorageService.GetItemAsync<string>(Constant.COMPUTERVISIONENDPOINT);

            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(computerVisionKey))
            { Endpoint = computerVisionEndpoint };

            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).Cast<VisualFeatureTypes>().ToList();
            List<Details> details = Enum.GetValues(typeof(Details)).Cast<Details>().ToList();

            ImageAnalysis results = await client.AnalyzeImageAsync(imageUrl, features, details);
            return results;
        }
    }
}
