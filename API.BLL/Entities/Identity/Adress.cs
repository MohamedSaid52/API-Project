using System.ComponentModel.DataAnnotations;

namespace API.BLL.Entities.Identity
{
    public class Adress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string sreet { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        [Required]
        public string appuserid { get; set; }
        public AppUser appUser { get; set; }
    }
}