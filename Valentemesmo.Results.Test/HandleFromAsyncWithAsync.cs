using System;
using System.Threading.Tasks;
using Xunit;
using ValenteMesmo.Results;
using Valentemesmo.Railway.Test;

namespace Valentemesmo.Test
{
    public class HandleFromAsyncWithAsync
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            async Task<Result<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Match(
                        async actual => await Task.FromResult(actual == expected)
                        , ex => false));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            async Task<Result<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Match(
                        f => false
                        , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
