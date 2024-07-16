
namespace Domain.Entities.ValueObjects
{
    public record Address
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public PostalCode PostalCode { get; set; }

    }
}