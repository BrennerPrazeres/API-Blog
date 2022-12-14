using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class EditorCategoryViewModel
    {
        //Validações
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Este campo deve conter de 3 a 40 catecteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "O slug é obrigatório")]
        public string Slug { get; set; }
    }
}
