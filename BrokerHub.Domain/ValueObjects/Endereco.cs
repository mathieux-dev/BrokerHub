using BrokerHub.Domain.Exceptions;

namespace BrokerHub.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public string Numero { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }
        public string? Complemento { get; private set; }

        public Endereco(string rua, string numero, string cidade, string estado, string cep, string? complemento = null)
        {
            ValidarRua(rua);
            ValidarNumero(numero);
            ValidarCidade(cidade);
            ValidarEstado(estado);
            ValidarCEP(cep);

            Rua = rua;
            Numero = numero;
            Cidade = cidade;
            Estado = estado;
            CEP = cep;
            Complemento = complemento;
        }

        private static void ValidarRua(string rua)
        {
            if (string.IsNullOrWhiteSpace(rua))
            {
                throw new DomainValidationException("A rua é obrigatória.");
            }
        }

        private static void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new DomainValidationException("O número é obrigatório.");
            }
        }

        private static void ValidarCidade(string cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade))
            {
                throw new DomainValidationException("A cidade é obrigatória.");
            }
        }

        private static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado) || estado.Length != 2)
            {
                throw new DomainValidationException("O estado deve ter 2 caracteres.");
            }
        }

        private static void ValidarCEP(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep) || !CEPValido(cep))
            {
                throw new DomainValidationException("O CEP informado é inválido.");
            }
        }

        private static bool CEPValido(string cep)
        {
            var regex = @"^\d{5}-?\d{3}$";
            return System.Text.RegularExpressions.Regex.IsMatch(cep, regex);
        }
    }
}
