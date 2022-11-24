using System.ComponentModel.DataAnnotations;

namespace Knucklebones.ViewModel
{
    public class UserDTO
    {
        public class RegisterModel
        {
            [Required(ErrorMessage = "Не указано Имя")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароль введен неверно")]
            public string ConfirmPassword { get; set; }
        }
        public class LoginModel
        {
            [Required(ErrorMessage = "Не указано Имя")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Не указан пароль")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
