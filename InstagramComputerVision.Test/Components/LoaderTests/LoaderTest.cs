using Bunit;

using InstagramComputerVision.Components;

using Xunit;

namespace InstagramComputerVision.Test
{
    public class LoaderTest : ComponentTestFixture
    {
        [Fact]
        public void LoaderRendersCorrectly()
        {
            var cut = RenderComponent<Loader>();

            Assert.Equal(@"<div class=""loader""></div>", cut.Markup);
        }
    }
}