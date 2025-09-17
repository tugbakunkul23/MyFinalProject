using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class User:IEntity
    {
        //public byte[] PasswordHash;
        //public byte[] PasswordSalt;
        //public bool Status;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }    // Hash olarak saklanacak
        public byte[] PasswordSalt { get; set; }    // Salt olarak saklanacak
        public string Email { get; set; }
        //public string Password { get; set; }
        public bool Status { get; set; }
    }
}
