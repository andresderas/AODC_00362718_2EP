using System;

namespace Segundo_parcial
{
    public class Apporder
    {
        public int idOrder { get; set; }
        public DateTime createDate { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string address { get; set; }

        public Apporder()
        {
            idOrder = 0;
            createDate = DateTime.Now;
            name = "";
            fullname = "";
            address = "";
        }
    }
}
