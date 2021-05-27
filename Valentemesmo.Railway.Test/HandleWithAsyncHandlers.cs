using System;
using System.Threading.Tasks;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleWithAsyncHandlers
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            Railway<string> sut() => expected;

            Assert.True(
                await sut()
                    .Handle(
                        async actual => await Task.FromResult(actual == expected)
                        , async ex => await Task.FromResult(false)));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            Railway<string> sut() => expected;

            Assert.True(
                await sut()
                    .Handle(
                        async ex => await Task.FromResult(false)
                        , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
