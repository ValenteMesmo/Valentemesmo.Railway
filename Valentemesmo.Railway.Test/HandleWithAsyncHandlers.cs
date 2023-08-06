using System;
using System.Threading.Tasks;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleWithAsyncHandlers
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            Result<string> sut() => expected;

            Assert.True(
                await sut()
                    .Match(
                        async actual => await Task.FromResult(actual == expected)
                        , async ex => await Task.FromResult(false)));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            Result<string> sut() => expected;

            Assert.True(
                await sut()
                    .Match(
                        async ex => await Task.FromResult(false)
                        , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
