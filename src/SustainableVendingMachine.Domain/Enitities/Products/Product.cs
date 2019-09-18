namespace SustainableVendingMachine.Domain.Enitities.Products
{
    public abstract class Product
    {
        public abstract string Name { get; }
        public abstract decimal Price { get; }
    }
}