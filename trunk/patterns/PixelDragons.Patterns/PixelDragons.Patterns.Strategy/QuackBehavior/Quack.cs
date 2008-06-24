using System;

namespace PixelDragons.Patterns.Strategy.QuackBehavior
{
    public class NormalQuack : IQuackBehavior
    {
        public string Quack()
        {
            return "Quack";
        }
    }
}
