using System.ComponentModel.DataAnnotations;

namespace Prova.Models
{
    public class Cad_Aluno
    {
        
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"\d{3}\.\d{3}\.\d{3}-\d{2}", ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "O e-mail deve estar em um formato válido.")]
        public string Email { get; set; }

        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}
