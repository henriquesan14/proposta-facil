using PropostaFacil.Application.Clients.Commands.CreateClient;

namespace PropostaFacil.Tests.Builders.Commands
{
    public class CreateClientCommandBuilder
    {
        private string _name = "Default Client";
        private string _document = "12345678901";
        private string _email = "client@example.com";
        private string _phoneNumber = "11999999999";
        private string _addressStreet = "Street";
        private string _addressNumber = "123";
        private string _addressComplement = "ap1";
        private string _addressDistrict = "district";
        private string _addressCity = "city";
        private string _addressState = "ST";
        private string _addressZipCode = "12345000";

        public CreateClientCommandBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CreateClientCommandBuilder WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public CreateClientCommandBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CreateClientCommandBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public CreateClientCommandBuilder WithAddressStreet(string street)
        {
            _addressStreet = street;
            return this;
        }

        public CreateClientCommandBuilder WithAddressNumber(string number)
        {
            _addressNumber = number;
            return this;
        }

        public CreateClientCommandBuilder WithAddressComplement(string complement)
        {
            _addressComplement = complement;
            return this;
        }

        public CreateClientCommandBuilder WithAddressDistrict(string district)
        {
            _addressDistrict = district;
            return this;
        }

        public CreateClientCommandBuilder WithAddressCity(string city)
        {
            _addressCity = city;
            return this;
        }

        public CreateClientCommandBuilder WithAddressState(string state)
        {
            _addressState = state;
            return this;
        }

        public CreateClientCommandBuilder WithAddressZipCode(string zipCode)
        {
            _addressZipCode = zipCode;
            return this;
        }

        public CreateClientCommand Build() =>
            new CreateClientCommand(
                _name,
                _document,
                _email,
                _phoneNumber,
                _addressStreet,
                _addressNumber,
                _addressComplement,
                _addressDistrict,
                _addressCity,
                _addressState,
                _addressZipCode
            );
    }

}
