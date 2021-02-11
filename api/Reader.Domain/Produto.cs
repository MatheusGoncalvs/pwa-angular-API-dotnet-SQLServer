using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;
using Reader.Core.DomainObjects;

namespace Reader.Domain
{
    public class Produto : Entity
    {
        public string Nome { get; private set; }
        public int Quantidade { get; private set; }
        public double ValorUnitario { get; private set; }
        [NotMapped]
        public double ValorTotal { get; private set; }
        public DateTime DataEntrega { get; private set; }
        public int IdLote { get; private set; }
        public virtual Lote IdLoteNavigation { get; private set; }

        public Produto(string nome, int quantidade, double valorUnitario, DateTime dataEntrega)
        {
            this.Nome = nome;
            this.Quantidade = quantidade;
            this.ValorUnitario = valorUnitario;
            this.DataEntrega = dataEntrega;

            CalcularValorTotal();
            EhValido();
        }

        public override bool EhValido()
        {
            ValidationResult = new ProdutoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public double CalcularValorTotal()
        {
            double valorTotal = Quantidade * ValorUnitario;
            ValorTotal = Math.Round(valorTotal, 2);
            return ValorTotal;
        }
    }
    public class ProdutoValidation : AbstractValidator<Produto>
    {

        public ProdutoValidation()
        {
            RuleFor(c => c.DataEntrega)
                .Must(DataVencimentoSuperiorAtual)
                .WithMessage("Todos os campos devem estar com a Data Entrega maior ou igual que a data atual.");
            RuleFor(c => c.Quantidade)
                .Must(QuantidadeDeveSerMaiorQueZero)
                .WithMessage("Todos os campos Quantidade devem ser maior do que zero.");
            RuleFor(c => c.Quantidade)
                .Must(ValorUnitarioDeveSerMaiorQueZero)
                .WithMessage("Todos os campos Valor Unitário devem ser maior do que zero.");
        }

        protected static bool DataVencimentoSuperiorAtual(DateTime dataEntrega)
        {
            return dataEntrega >= DateTime.Now;
        }
        protected static bool QuantidadeDeveSerMaiorQueZero(int quantidade)
        {
            return quantidade > 0;
        }
        protected static bool ValorUnitarioDeveSerMaiorQueZero(int valorUnitario)
        {
            return valorUnitario > 0;
        }
    }
}