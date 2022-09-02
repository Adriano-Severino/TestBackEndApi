namespace TestBackEndApi.Domain
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CpfCnpj { get; set; }
        public string Telephone { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public String Registration { get; set; }
    }
}
