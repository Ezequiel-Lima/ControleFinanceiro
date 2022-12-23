namespace ControleFinanceiro.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public bool Tipo { get; set; } 
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
