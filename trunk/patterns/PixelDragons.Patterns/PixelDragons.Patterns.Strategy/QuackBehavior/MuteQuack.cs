using System;

namespace PixelDragons.Patterns.Strategy.QuackBehavior
{
    public class MuteQuack : IQuackBehavior
    {
        public string Quack()
        {
            return "<< Silence >>";
        }
    }
}
