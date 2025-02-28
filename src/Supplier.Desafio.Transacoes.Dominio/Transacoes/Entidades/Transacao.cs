using Supplier.Desafio.Transacoes.Dominio.Core;

namespace Supplier.Desafio.Transacoes.Dominio.Transacoes.Entidades
{
    public class Transacao : Entity
    {
        public virtual Guid IdTransacao { get; protected set; }
        public virtual int IdCliente { get; protected set; }
        public virtual decimal ValorSimulacao { get; protected set; }
        public virtual string Status { get; protected set; } = string.Empty;

        public Transacao() { }

        public Transacao(int idCliente, decimal valorSimulacao, string status)
        {
            SetIdTransacao();
            SetIdCliente(idCliente);
            SetValorSimulacao(valorSimulacao);
            SetStatus(status);
        }

        public virtual void SetIdTransacao()
        {
            IdTransacao = Guid.NewGuid();
        }

        public virtual void SetIdCliente(int idCliente)
        {
            if (idCliente <= 0)
                throw new DomainException("Id do cliente inválido");

            IdCliente = idCliente;
        }

        public virtual void SetValorSimulacao(decimal valorSimulacao)
        {
            if (valorSimulacao <= 0)
                throw new DomainException("Valor da simulação inválido");

            ValorSimulacao = valorSimulacao;
        }

        public virtual void SetStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new DomainException("Status inválido");

            Status = status;
        }
    }
}
