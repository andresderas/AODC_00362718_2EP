namespace Segundo_parcial
{
    public class Address
    {
        public int idAddress { get; set; }
        public int idUser { get; set; }
        public string address { get; set; }

        public Address()
        {
            idAddress = 0;
            idUser = 0;
            address = "";
        }
    }
}
