using System;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinChangingTypeTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(int expected, string anything)
        {
            Result<string> first() => anything;
            Result<int> second() => expected;

            var actual = first()
                .Pipe(f => second());

            Assert.Equal((int)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected)
        {
            Result<int> first() => 0;
            Result<string> second() => expected;

            var actual = first()
                .Pipe(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
