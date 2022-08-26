using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class SignUp
    {
        [Required, RegularExpression(@"^([a-zA-ZàèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸçÇßØøÅåÆæœ'`'\-]{2,})+$")]
        public string FirstName { get; set; } = null!;

        [Required, RegularExpression(@"^([ a-zA-ZàèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸçÇßØøÅåÆæœ'`'\-]{2,})+$")]
        public string LastName { get; set; } = null!;

        [Required, RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z")]
        public string Email { get; set; } = null!;

        [Required, RegularExpression(@"((?=.*\d)(?=.*[A-Z]).{8,})")]
        public string Password { get; set; } = null!;
    }
}
