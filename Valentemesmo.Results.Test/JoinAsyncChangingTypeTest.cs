using System;
using System.Threading.Tasks;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinAsyncChangingTypeTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(int expected, string anything)
        {
            Result<string> first() => anything;
            async Task<Result<int>> second() => 
                await Task.FromResult(expected);

            var actual = await first()
                .Pipe(async f => await second());

            Assert.Equal((int)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected)
        {
            Result<int> first() => 0;
            async Task<Result<string>> second() => await Task.FromResult(expected);

            var actual = await first()
                .Pipe(async f => await second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
