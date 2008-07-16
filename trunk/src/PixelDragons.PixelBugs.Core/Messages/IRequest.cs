namespace PixelDragons.PixelBugs.Core.Messages
{
    public interface IRequest
    {
        //We need to supply a default constructor as we don't know where this request may be created (eg MonoRail DataBind)
        //therefore we need a separate validation method that can be called in the services;
        void Validate();
    }
}