using System;
using System.ComponentModel.DataAnnotations;

namespace api.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MaxLength(50, ErrorMessage = "O campo {0} só pode ter no máximo {1} campos.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double ValorUnitario { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataEntrega { get; set; }
        public double ValorTotal { get; set; }
    }
}