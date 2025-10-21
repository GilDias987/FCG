namespace FCG.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public void SetarDataCriacao(DateTime dataCricao, DateTime dataAtualizacao)
        {
            DataCriacao = dataCricao;
            DataAtualizacao = dataAtualizacao;
        }

        public void SetarDataAtualizacao(DateTime dataAtualizacao)
        {
            DataAtualizacao = dataAtualizacao;
        }
    }
}
