using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Pages.Models
{
    public class User : DTO.User
    {
        [DisplayName("Name")]
        public override string Name { get => base.Name; set => base.Name = value; }

        [DisplayName("Email address")]
        [DataType(DataType.EmailAddress)]
        public override string EmailAddress { get => base.EmailAddress; set => base.EmailAddress = value; }
    }
}
