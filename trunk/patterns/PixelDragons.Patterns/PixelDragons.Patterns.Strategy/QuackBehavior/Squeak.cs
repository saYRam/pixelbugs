using System;

namespace PixelDragons.Patterns.Strategy.QuackBehavior
{
    public class Squeak : IQuackBehavior
    {
        public string Quack()
        {
            return "Squeak";
        }
    }
}
