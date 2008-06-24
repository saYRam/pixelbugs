using System;
using System.Collections.Generic;
using System.Text;
using PixelDragons.Patterns.Strategy.QuackBehavior;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy
{
    public class RubberDuck : Duck
    {
        public RubberDuck()
        {
            quackBehavior = new Squeak();
            flyBehavior = new FlyNoWay();
        }

        public override string Display()
        {
            return "Displaying of a rubber duck is unique";
        }
    }
}
