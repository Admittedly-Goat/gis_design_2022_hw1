namespace MyMapObjects
{
    public abstract class moRenderer
    {
        public abstract moRendererTypeConstant RendererType { get; }
        public abstract moRenderer Clone();
    }
}
