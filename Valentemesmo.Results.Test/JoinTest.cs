using System;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(string expected, string anything)
        {
            Result<string> first() => anything;
            Result<string> second() => expected;

            var actual = first()
                .Pipe(f => second());

            Assert.Equal((string)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected, string anything)
        {
            Result<string> first() => anything;
            Result<string> second() => expected;

            var actual = first()
                .Pipe(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
