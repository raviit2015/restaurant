
namespace ECommerce.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }

        public int Id { get; set; }
        public string Token { get; set; }

        public int DummyID { get; set; }

        public string Name { get; set; }

        public string Details{ get; set; }


    }
}
