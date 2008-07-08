using Castle.MonoRail.Framework;

namespace PixelDragons.Commons.TestSupport
{
    public class FilterUnitTestBase : ControllerUnitTestBase
    {
        protected Controller _controller;
        protected IFilter _filter;

        protected bool ExecuteFilter()
        {
            return _filter.Perform(ExecuteWhen.BeforeAction, _controller.Context, _controller, _controller.ControllerContext);
        }
    }
}
