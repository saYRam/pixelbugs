﻿using System;
using System.Configuration;
using System.Threading;
using MbUnit.Framework;

namespace PixelDragons.Commons.TestSupport
{
    [TestFixture(ApartmentState = ApartmentState.STA)]
    public abstract class AcceptanceTestBase : TestFixtureBase
    {
        protected string BuildUrl(string controller, string action)
        {
            return String.Format("http://{0}:{1}/{2}/{3}.{4}", this.Server, this.Port, controller, action, this.Extension);
        }
    }
}