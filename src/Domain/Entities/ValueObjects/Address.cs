using Domain.Bases;

namespace Domain.Entities.ValueObjects
{
    public class Address : ValueObject
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return State;
            yield return City;
            yield return Street;
            yield return PostalCode;
        }
    }
}