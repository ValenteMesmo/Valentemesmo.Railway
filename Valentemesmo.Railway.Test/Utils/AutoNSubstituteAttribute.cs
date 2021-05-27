using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;

namespace Valentemesmo.Railway.Test
{
    public class AutoNSubstituteAttribute : AutoDataAttribute
    {
        public AutoNSubstituteAttribute()
            : base(() =>
            {
                var fixture = new Fixture();
                fixture.Customize(new AutoNSubstituteCustomization());
                return fixture;
            })
        {
        }
    }

    public class InlineNSubstituteAttribute : InlineAutoDataAttribute
    {
        public InlineNSubstituteAttribute(params object[] values) : base(new AutoNSubstituteAttribute(), values)
        {
        }
    }
}
