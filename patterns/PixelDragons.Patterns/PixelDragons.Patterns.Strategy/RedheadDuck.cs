using System;
using System.Collections.Generic;
using System.Text;
using PixelDragons.Patterns.Strategy.QuackBehavior;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy
{
    public class RedheadDuck : Duck
    {
        public RedheadDuck()
        {
            quackBehavior = new NormalQuack();
            flyBehavior = new FlyWithWings();
        }

        public override string Display()
        {
            return "Displaying of a redhead duck is unique";
        }
    }
}
