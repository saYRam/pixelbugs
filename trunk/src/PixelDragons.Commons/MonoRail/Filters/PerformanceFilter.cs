using System;
using System.Reflection;

using Castle.MonoRail.Framework;

using PixelDragons.Commons.IO;

namespace PixelDragons.Commons.MonoRail.Filters
{
    public class PerformanceFilter : IFilter
    {
        public bool Perform(ExecuteEnum exec, IRailsEngineContext context, Controller controller)
        {
            controller.PropertyBag["version"] = Assembly.GetAssembly(controller.GetType()).GetName().Version.ToString();

            if (exec == ExecuteEnum.StartRequest)
            {
                controller.Logger.DebugFormat("Start Request: {0}/{1}", controller.Name, controller.Action);
                controller.PropertyBag["requestStart"] = DateTime.Now;
            }
            else if (exec == ExecuteEnum.AfterRendering)
            {
                controller.Logger.DebugFormat("End Request: {0}/{1}", controller.Name, controller.Action);

                DateTime? requestStart = (DateTime?)controller.PropertyBag["requestStart"];

                if (requestStart != null && requestStart.HasValue)
                {
                    TimeSpan duration = new TimeSpan(DateTime.Now.Ticks - requestStart.Value.Ticks);
                    double seconds = (duration.TotalMilliseconds / 1000.0);
                    context.UnderlyingContext.Response.Filter = new FindReplaceStream(context.UnderlyingContext.Response.Filter, context.UnderlyingContext.Response.ContentEncoding, "[T]", seconds.ToString("F3"));
                }
            }

            return true;
        }
    }
}
