using PropostaFacil.Application.Tenants.Commands.CreateTenant;

namespace PropostaFacil.Tests.Builders.Commands
{
    public class CreateTenantCommandBuilder
    {
        private string _name = "Default Tenant";
        private string _domain = "default.com";
        private string _document = "12345678000199";
        private string _email = "tenant@example.com";
        private string _phoneNumber = "11999999999";
        private string _street = "Street";
        private string _number = "123";
        private string _complement = "ap1";
        private string _district = "district";
        private string _city = "city";
        private string _state = "ST";
        private string _zipCode = "12345000";

        public CreateTenantCommandBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CreateTenantCommandBuilder WithDomain(string domain)
        {
            _domain = domain;
            return this;
        }

        public CreateTenantCommandBuilder WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public CreateTenantCommandBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CreateTenantCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public CreateTenantCommandBuilder WithStreet(string street)
        {
            _street = street;
            return this;
        }

        public CreateTenantCommandBuilder WithNumber(string number)
        {
            _number = number;
            return this;
        }

        public CreateTenantCommandBuilder WithComplement(string complement)
        {
            _complement = complement;
            return this;
        }

        public CreateTenantCommandBuilder WithDistrict(string district)
        {
            _district = district;
            return this;
        }

        public CreateTenantCommandBuilder WithCity(string city)
        {
            _city = city;
            return this;
        }

        public CreateTenantCommandBuilder WithState(string state)
        {
            _state = state;
            return this;
        }

        public CreateTenantCommandBuilder WithZipCode(string zipCode)
        {
            _zipCode = zipCode;
            return this;
        }

        public CreateTenantCommand Build() =>
            new CreateTenantCommand(
                _name,
                _domain,
                _document,
                _email,
                _phoneNumber,
                _street,
                _number,
                _complement,
                _district,
                _city,
                _state,
                _zipCode);
    }

}
