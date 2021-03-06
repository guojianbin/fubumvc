using FubuMVC.Core.Navigation;
using NUnit.Framework;
using Shouldly;

namespace FubuMVC.Tests.Navigation
{
    [TestFixture]
    public class MenuItemStateTester
    {
        [Test]
        public void least_method_empty_is_available()
        {
            MenuItemState.Least().ShouldBe(MenuItemState.Available);
        }

        [Test]
        public void least_method_with_data()
        {
            MenuItemState.Least(MenuItemState.Available).ShouldBe(MenuItemState.Available);
            MenuItemState.Least(MenuItemState.Hidden).ShouldBe(MenuItemState.Hidden);
            MenuItemState.Least(MenuItemState.Disabled).ShouldBe(MenuItemState.Disabled);

            MenuItemState.Least(MenuItemState.Hidden, MenuItemState.Available).ShouldBe(MenuItemState.Hidden);
            MenuItemState.Least(MenuItemState.Hidden, MenuItemState.Available, MenuItemState.Disabled).ShouldBe(MenuItemState.Hidden);
            MenuItemState.Least(MenuItemState.Disabled, MenuItemState.Available).ShouldBe(MenuItemState.Disabled);

            MenuItemState.Least(MenuItemState.Hidden, MenuItemState.Disabled).ShouldBe(MenuItemState.Hidden);
        }

        [Test]
        public void hidden_is_not_visible_and_disabled()
        {
            MenuItemState.Hidden.IsEnabled.ShouldBeFalse();
            MenuItemState.Hidden.IsShown.ShouldBeFalse();
        }

        [Test]
        public void disabled_is_visible_but_disabled()
        {
            MenuItemState.Disabled.IsEnabled.ShouldBeFalse();
            MenuItemState.Disabled.IsShown.ShouldBeTrue();
        }

        [Test]
        public void available_is_enabled_and_shown()
        {
            MenuItemState.Available.IsEnabled.ShouldBeTrue();
            MenuItemState.Available.IsShown.ShouldBeTrue();
        }

        [Test]
        public void active_is_enabled_and_shown()
        {
            MenuItemState.Active.IsEnabled.ShouldBeTrue();
            MenuItemState.Active.IsShown.ShouldBeTrue();
        }
    }
}