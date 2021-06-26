using System;
using ValenteMesmo.Railway;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class JoinChangingTypeTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(int expected, string anything)
        {
            Railway<string> first() => anything;
            Railway<int> second() => expected;

            var actual = first()
                .Join(f => second());

            Assert.Equal((int)actual, expected);
            Assert.Null((Exception)actual);
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected)
        {
            Railway<int> first() => 0;
            Railway<string> second() => expected;

            var actual = first()
                .Join(f => second());

            Assert.Equal((Exception)actual, expected);
            Assert.Null((string)actual);
        }
    }
}
