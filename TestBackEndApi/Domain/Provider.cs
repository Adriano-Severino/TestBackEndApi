using System.ComponentModel.DataAnnotations;

namespace TestBackEndApi.Domain
{
    public class Provider : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CpfCnpj { get; set; }
        public string? Rg { get; set; }
        public string Telephone { get; set; }
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime Registered { get; set; }
    }
}
