using System;
using System.Collections.Generic;
using System.Text;
using PixelDragons.Patterns.Strategy.QuackBehavior;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy
{
    public class MallardDuck : Duck
    {
        public MallardDuck()
        {
            quackBehavior = new NormalQuack();
            flyBehavior = new FlyWithWings();
        }

        public override string Display()
        {
            return "Displaying of a mallard duck is unique";
        }
    }
}
