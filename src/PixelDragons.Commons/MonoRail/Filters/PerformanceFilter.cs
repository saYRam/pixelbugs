using System;
using System.Reflection;
using Castle.MonoRail.Framework;
using PixelDragons.Commons.IO;

namespace PixelDragons.Commons.MonoRail.Filters
{
    public class PerformanceFilter : IFilter
    {
        public bool Perform(ExecuteWhen exec, IEngineContext context, IController controller, IControllerContext controllerContext)
        {
            controllerContext.PropertyBag["version"] = Assembly.GetAssembly(controller.GetType()).GetName().Version.ToString();

            if (exec == ExecuteWhen.BeforeAction)
            {
                controllerContext.PropertyBag["requestStart"] = DateTime.Now;
            }
            else if (exec == ExecuteWhen.AfterRendering)
            {
                DateTime? requestStart = (DateTime?) controllerContext.PropertyBag["requestStart"];

                if (requestStart != null && requestStart.HasValue)
                {
                    TimeSpan duration = new TimeSpan(DateTime.Now.Ticks - requestStart.Value.Ticks);
                    double seconds = (duration.TotalMilliseconds/1000.0);
                    context.UnderlyingContext.Response.Filter =
                        new FindReplaceStream(context.UnderlyingContext.Response.Filter,
                                              context.UnderlyingContext.Response.ContentEncoding, "[T]",
                                              seconds.ToString("F3"));
                }
            }

            return true;
        }
    }
}