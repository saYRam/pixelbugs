namespace PixelDragons.Patterns.Strategy.FlyBehavior
{
	public class FlyNoWay : IFlyBehavior
	{
		public string Fly()
		{
            return "I can't fly";
		}
	}
}