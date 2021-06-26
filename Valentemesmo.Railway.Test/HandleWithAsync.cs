using System;
using System.Threading.Tasks;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleWithAsync
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            Railway<string> sut() => expected;

            Assert.True(await sut().Handle(
                async actual => await Task.FromResult(actual == expected)
                , ex => false));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            Railway<string> sut() => expected;

            Assert.True(await sut().Handle(
                f => false
                , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
