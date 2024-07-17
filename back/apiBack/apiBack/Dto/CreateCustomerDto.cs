using System.ComponentModel.DataAnnotations;

namespace apiBack.Dto
{
    public class CreateCustomerDto
    {
        [Required(ErrorMessage = "El campo es requerido")]
        public string firstName { get; set; }
            
        [Required(ErrorMessage = "El campo es requerido")]
        public string lastName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "el email no es correcto")]
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
    }
}
