using System.ComponentModel.DataAnnotations;

namespace TestBackEndApi.Models
{
    public class Provider
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CpfCnpj { get; set; }
        public string Telephone { get; set; }
        public Company Company { get; set; }

        [DataType(DataType.Date)]
        public DateTime CustomData { get; set; }
    }
}
