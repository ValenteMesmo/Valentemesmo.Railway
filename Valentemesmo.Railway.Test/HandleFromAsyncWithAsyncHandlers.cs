using System;
using System.Threading.Tasks;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleFromAsyncWithAsyncHandlers
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            async Task<Railway<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Handle(
                        async actual => await Task.FromResult(actual == expected)
                        , async ex => await Task.FromResult(false)));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            async Task<Railway<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Handle(
                        async f => await Task.FromResult(false)
                        , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
