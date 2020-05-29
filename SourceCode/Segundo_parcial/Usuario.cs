namespace Segundo_parcial
{
    public class Usuario
    {
        public int idUser { get; set; }
        public string fullname { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool userType { get; set; }

        public Usuario()
        {
            idUser = 0;
            fullname = "";
            username = "";
            password = "";
            userType = false;
        }
    }
}
