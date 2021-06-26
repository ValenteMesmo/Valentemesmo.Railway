using System;
using System.Threading.Tasks;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinAsyncChangingTypeTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(int expected, string anything)
        {
            Railway<string> first() => anything;
            async Task<Railway<int>> second() => 
                await Task.FromResult(expected);

            var actual = await first()
                .Join(async f => await second());

            Assert.Equal((int)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            Railway<int> first() => 0;
            async Task<Railway<string>> second() => await Task.FromResult(expected);

            var actual = await first()
                .Join(async f => await second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
