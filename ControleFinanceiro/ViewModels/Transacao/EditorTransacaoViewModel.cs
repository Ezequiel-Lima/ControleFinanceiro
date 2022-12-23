using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.ViewModels.Transacao
{
    public class EditorTransacaoViewModel
    {
        [MinLength(3)]
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public bool Tipo { get; set; }

        public EditorTransacaoViewModel()
        {
            Tipo = true;
        }
    }
}
