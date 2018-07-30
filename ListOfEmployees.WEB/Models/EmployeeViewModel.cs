using System;
using System.ComponentModel.DataAnnotations;

namespace ListOfEmployees.WEB.Models
{
    public class EmployeeViewModel
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя обязательно для заполнения!")]
        [StringLength(50,MinimumLength = 4,ErrorMessage = "Длинна строки должна быть от 4 до 50 символов!")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фвмилия обязательна для заполения!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длинна строки должна быть от 4 до 50 символов!")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "День рождения обязателен для заполнения!")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime),"10.10.1900","30.12.3000",ErrorMessage = "Диапазон дат 10.10.1900-30.12.3000")]
        [Display(Name = "День рождения")]
        public DateTime Birthday { get; set; }
    }
}