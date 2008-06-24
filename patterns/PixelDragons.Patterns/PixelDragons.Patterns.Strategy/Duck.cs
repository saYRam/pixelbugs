using System;
using PixelDragons.Patterns.Strategy.QuackBehavior;
using PixelDragons.Patterns.Strategy.FlyBehavior;

namespace PixelDragons.Patterns.Strategy
{
    public abstract class Duck
    {
        protected IFlyBehavior flyBehavior;
        protected IQuackBehavior quackBehavior;

        public IFlyBehavior FlyBehavior
        {
            get { return flyBehavior; }
            set { flyBehavior = value; }
        }

        public IQuackBehavior QuackBehavior
        {
            get { return quackBehavior; }
            set { quackBehavior = value; }
        }

        public abstract string Display();

        public string PerformFly()
        {
            return flyBehavior.Fly();
        }

        public string PerformQuack()
        {
            return quackBehavior.Quack();
        }

        public string Swim()
        {
            return "All ducks float, even decoys!";
        }
    }
}
