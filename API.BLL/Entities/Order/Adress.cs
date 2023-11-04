using API.BLL.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace API.BLL.Entities.Order
{
    public class Adress
    {
        public Adress(string firstName, string lastName, string sreet, string city, string state, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            this.sreet = sreet;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string sreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
      
    }
}