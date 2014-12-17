namespace Application.Backend.Domain.Application
{
    public class Address
    {
        public Address(string houseName,
                       string houseNumber,
                       string flat,
                       string street,
                       string street2,
                       string townOrCity,
                       string district,
                       string county,
                       string postcode)
        {
            _houseName = houseName;
            _houseNumber = houseNumber;
            _flat = flat;
            _street = street;
            _street2 = street2;
            _townOrCity = townOrCity;
            _district = district;
            _county = county;
            _postcode = postcode;
        }

        public string County
        {
            get
            {
                return _county;
            }
        }

        public string District
        {
            get
            {
                return _district;
            }
        }

        public string Flat
        {
            get
            {
                return _flat;
            }
        }

        public string HouseName
        {
            get
            {
                return _houseName;
            }
        }

        public string HouseNumber
        {
            get
            {
                return _houseNumber;
            }
        }

        public string Postcode
        {
            get
            {
                return _postcode;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }
        }

        public string Street2
        {
            get
            {
                return _street2;
            }
        }

        public string TownOrCity
        {
            get
            {
                return _townOrCity;
            }
        }

        private readonly string _county;

        private readonly string _district;

        private readonly string _flat;

        private readonly string _houseName;

        private readonly string _houseNumber;

        private readonly string _postcode;

        private readonly string _street;

        private readonly string _street2;

        private readonly string _townOrCity;
    }
}