using System;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(string expected, string anything)
        {
            Railway<string> first() => anything;
            Railway<string> second() => expected;

            var actual = first()
                .Join(f => second());

            Assert.Equal((string)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected, string anything)
        {
            Railway<string> first() => anything;
            Railway<string> second() => expected;

            var actual = first()
                .Join(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
