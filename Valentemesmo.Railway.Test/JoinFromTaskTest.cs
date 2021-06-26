using System;
using System.Threading.Tasks;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinFromTaskTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected, string anything)
        {
            async Task<Railway<string>> first() => 
                await Task.FromResult(anything);
            Railway<string> second() => expected;

            var actual = await first()
                .Join(f => second());

            Assert.Equal((string)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected, string anything)
        {
            async Task<Railway<string>> first() =>
                await Task.FromResult(anything);
            Railway<string> second() => expected;

            var actual = await first()
                .Join(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
