using System;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(string expected)
        {
            Railway<string> sut() => expected;

            Assert.True(sut().Handle(
                actual => actual == expected
                , ex => false));
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected)
        {
            Railway<string> sut() => expected;

            Assert.True(sut().Handle(
                f => false
                , actual => actual == expected));
        }
    }
}
