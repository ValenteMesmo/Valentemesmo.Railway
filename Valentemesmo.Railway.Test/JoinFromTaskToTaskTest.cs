using System;
using System.Threading.Tasks;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinFromTaskToTaskTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected, string anything)
        {
            async Task<Railway<string>> first() =>
                await Task.FromResult(anything);
            async Task<Railway<string>> second() => 
                await Task.FromResult(expected);

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
            async Task<Railway<string>> second() =>
                await Task.FromResult(expected);

            var actual = await first()
                .Join(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
