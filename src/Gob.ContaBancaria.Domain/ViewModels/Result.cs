using System;

namespace Gob.ContaBancaria.Domain.ViewModels
{
    public static class Result
    {
        public static SuccessResult DepositoEfetuado() => new("Deposito efetuado com sucesso!");
        public static SuccessResult SaqueEfetuado() => new("Saque efetuado com sucesso!");
        public static SuccessResult TransferenciaEfetuada() => new("Transferência realizado com sucesso!");
        public static SuccessResult ContaCriada() => new("Conta bancaria criada com sucesso!");

        public static ErrorResult ContaNaoExiste(int idConta) => new($"A conta bancaria '{idConta}' não existe.");
        public static ErrorResult ContaNaoExisteOuSemDeposito(int idConta) => new($"A Conta '{idConta}' não existe, ou ainda não foi feito nenhum deposito.");
        public static ErrorResult NenhumaContaEncontrada() => new("Nenhuma conta encontrada.");
        public static ErrorResult SaldoInsuficiente(decimal saldoNecessario, decimal saldoDisponivel) => new($"A conta bancaria não possui saldo suficiente para realizar esta operação. Valor da operação com taxas inclusas: ‘{saldoNecessario}’, Saldo disponível na conta: ‘{saldoDisponivel}’");
        public static ErrorResult TransferenciaParaMesmaConta() => new("A conta de origem não pode ser a mesma de destino.");

        public static SuccessResult<T> SuccessResult<T>(T response) => new(response);
    }
}
