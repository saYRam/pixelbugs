using Castle.MonoRail.Framework;

namespace PixelDragons.Commons.TestSupport
{
    public class FilterUnitTestBase : ControllerUnitTestBase
    {
        protected Controller controller;
        protected IFilter filter;

        protected bool ExecuteFilter()
        {
            return filter.Perform(ExecuteWhen.BeforeAction, controller.Context, controller, controller.ControllerContext);
        }
    }
}