namespace TestBackEndApi.Domain
{
    public abstract partial class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
