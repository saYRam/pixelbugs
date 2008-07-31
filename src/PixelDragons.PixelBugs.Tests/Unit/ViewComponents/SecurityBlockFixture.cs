using System;
using System.Collections.Generic;
using Castle.MonoRail.TestSupport;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using PixelDragons.PixelBugs.Core.Domain;
using PixelDragons.PixelBugs.Core.DTOs;
using PixelDragons.PixelBugs.Web.ViewComponents;

namespace PixelDragons.PixelBugs.Tests.Unit.ViewComponents
{
    [TestFixture]
    public class When_using_the_security_block_component : BaseViewComponentTest
    {
        private SecurityBlockComponent component;

        [SetUp]
        public void SetUp()
        {
            component = new SecurityBlockComponent {Permission = "CreateCards"};
            PrepareViewComponent(component);
        }

        [TearDown]
        public void TearDown()
        {
            CleanUp();
        }

        [Test]
        public void Should_render_the_surrounded_block_if_the_current_user_has_the_required_permission()
        {
            Context.CurrentUser = new UserPermissionsDTO(Guid.Empty, new List<Permission> { Permission.CreateCards });

            component.Render();
            
            Assert.That(component.Rendered, Is.True);
        }

        [Test]
        public void Should_not_render_the_surrounded_block_if_the_current_user_does_not_have_the_required_permission()
        {
            Context.CurrentUser = new UserPermissionsDTO(Guid.Empty, new List<Permission>());

            component.Render();

            Assert.That(component.Rendered, Is.False);
        }

        [Test]
        public void Should_not_render_the_surrounded_block_if_there_is_no_current_user()
        {
            Context.CurrentUser = null;

            component.Render();

            Assert.That(component.Rendered, Is.False);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Should_throw_an_exception_if_an_invalid_permission_string_is_set()
        {
            component = new SecurityBlockComponent { Permission = "AN INVALID PERMISSION" };
            PrepareViewComponent(component);

            Context.CurrentUser = null;

            component.Render();
        }
    }
}