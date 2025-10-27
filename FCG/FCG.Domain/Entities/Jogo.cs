using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;
using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities
{
    public class Jogo : BaseEntity
    {
        #region Propriedades Base
        public string Titulo { get; private set; }
        public string? Descricao { get; private set; }
        public decimal? Preco { get; private set; }
        public decimal? Desconto { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public int PlataformaId { get; set; }
        public Plataforma Plataforma { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion

        private void ValidarDesconto(decimal desconto)
        {
            Guard.Against<DomainException>(desconto < 0 || desconto > 100, "O percentual do desconto não pode ser negativo ou maior que 100");
        }

        public Jogo()
        { 
        }

        public Jogo(string titulo, string descricao, decimal preco, decimal desconto, int generoId, int plataformaId)
        {
            Inicializar(titulo,descricao,preco,desconto,generoId,plataformaId);
        }

        public void Inicializar(string titulo, string descricao, decimal preco, decimal desconto, int generoId, int plataformaId)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(titulo), "O titulo do jogo não pode ser vazio.");
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(descricao), "A descricao do jogo não pode ser vazia.");
            Guard.Against<DomainException>(descricao.Length < 5, "A descricao deve possuir mais que 5 caracteres");
            Guard.Against<DomainException>(preco < 0, "O preço do jogo não pode ser menor que 0");
            Guard.AgainstEmptyId(generoId, "Genero Id");
            Guard.AgainstEmptyId(plataformaId, "Plataforma Id");

            ValidarDesconto(desconto);

            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            Desconto = desconto;
            GeneroId = generoId;
            PlataformaId = plataformaId;

        }

        public decimal CalcularPrecoComDesconto()
        {
            if (Desconto.HasValue && Preco.HasValue)
            {
                var descontoValor = (Preco.Value * Desconto.Value) / 100;
                return Preco.Value - descontoValor;
            }

            return Preco ?? 0;
        }

        public void AplicarDesconto(decimal novoDesconto)
        {
            ValidarDesconto(novoDesconto);
            Desconto = novoDesconto;
        }
    }
}
