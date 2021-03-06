using FubuMVC.Core.UI.Elements;
using FubuMVC.Core.Validation;
using FubuMVC.Core.Validation.Web.UI;
using NUnit.Framework;
using Shouldly;

namespace FubuMVC.Tests.Validation.Web.UI
{
    [TestFixture]
    public class MaxValueModifierTester : ValidationElementModifierContext<MaxValueModifier>
    {
        [Test]
        public void adds_the_max_data_attribute_for_min_value_rule()
        {
            var theRequest = ElementRequest.For(new TargetWithMaxValue(), x => x.Value);
            tagFor(theRequest).Data("max").ShouldBe(20);
        }

        [Test]
        public void no_data_attribute_when_rule_does_not_exist()
        {
            var theRequest = ElementRequest.For(new TargetWithNoMaxValue(), x => x.Value);
            tagFor(theRequest).Data("max").ShouldBeNull();
        }


        public class TargetWithMaxValue
        {
            [MaxValue(20)]
            public string Value { get; set; }
        }

        public class TargetWithNoMaxValue
        {
            public string Value { get; set; }
        }
    }
}