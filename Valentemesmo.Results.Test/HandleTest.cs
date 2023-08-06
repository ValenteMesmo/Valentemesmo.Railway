using System;
using ValenteMesmo.Results;
using Xunit;

namespace Valentemesmo.Railway.Test
{
    public class HandleTest
    {
        [Theory, AutoNSubstitute]
        public void HappyPath(string expected)
        {
            Result<string> sut() => expected;

            Assert.True(sut().Match(
                actual => actual == expected
                , ex => false));
        }

        [Theory, AutoNSubstitute]
        public void SadPath(Exception expected)
        {
            Result<string> sut() => expected;

            Assert.True(sut().Match(
                f => false
                , actual => actual == expected));
        }
    }
}
