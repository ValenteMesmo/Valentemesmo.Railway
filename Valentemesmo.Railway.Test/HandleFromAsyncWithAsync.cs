using System;
using System.Threading.Tasks;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleFromAsyncWithAsync
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected)
        {
            async Task<Railway<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Handle(
                        async actual => await Task.FromResult(actual == expected)
                        , ex => false));
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            async Task<Railway<string>> sut() => await Task.FromResult(expected);

            Assert.True(
                await sut()
                    .Handle(
                        f => false
                        , async actual => await Task.FromResult(actual == expected)));
        }
    }
}
