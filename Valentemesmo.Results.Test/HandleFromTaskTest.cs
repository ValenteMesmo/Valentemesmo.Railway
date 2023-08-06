using System;
using System.Threading.Tasks;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleFromTaskTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            async Task<Result<string>> sut() =>
                await Task.FromResult(expected);

            Assert.True(await sut().Match(
                actual => actual == expected
                , ex => false));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            async Task<Result<string>> sut() =>
                await Task.FromResult(expected);

            Assert.True(await sut().Match(
                f => false
                , actual => actual == expected));
        }
    }
}
