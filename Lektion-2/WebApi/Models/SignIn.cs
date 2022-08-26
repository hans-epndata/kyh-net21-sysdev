using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class SignIn
    {
        [Required, RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z")]
        public string Email { get; set; } = null!;

        [Required, RegularExpression(@"((?=.*\d)(?=.*[A-Z]).{8,})")]
        public string Password { get; set; } = null!;
    }
}
