namespace PoolImplementation
{
    public class ExampleEncapsulatedObject<T> where T : class, new()
    {
        public bool InUse { get; set; }
        public T ExObject { get; set; }

        public ExampleEncapsulatedObject()
        {
            this.InUse = false;
            this.ExObject = new T();
        }
    }
}
