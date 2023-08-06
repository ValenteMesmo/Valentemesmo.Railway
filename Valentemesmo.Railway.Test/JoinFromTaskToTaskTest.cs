using System;
using System.Threading.Tasks;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinFromTaskToTaskTest
    {
        [Theory, AutoNSubstitute]
        public async Task HappyPath(string expected, string anything)
        {
            async Task<Result<string>> first() =>
                await Task.FromResult(anything);
            async Task<Result<string>> second() => 
                await Task.FromResult(expected);

            var actual = await first()
                .Pipe(f => second());

            Assert.Equal((string)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public async Task SadPath(Exception expected, string anything)
        {
            async Task<Result<string>> first() =>
                await Task.FromResult(anything);
            async Task<Result<string>> second() =>
                await Task.FromResult(expected);

            var actual = await first()
                .Pipe(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
