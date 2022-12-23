using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.ViewModels.Transacao
{
    public class CreateTransacaoViewModel
    {
        [MinLength(3, ErrorMessage = "É necessário pelo menos 3 caracteres")]
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public bool Tipo { get; set; }
    }
}
