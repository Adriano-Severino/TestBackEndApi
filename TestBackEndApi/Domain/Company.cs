namespace TestBackEndApi.Domain
{
    public class Company : BaseEntity
    {
        public string FantasyName { get; set; }
        public string Cnpj { get; set; }
        public string Uf { get; set; }
        public ICollection<Provider> Providers { get; set; }
    }
}
