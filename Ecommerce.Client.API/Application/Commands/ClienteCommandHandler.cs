using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Clients.API.Application.Events;
using Ecommerce.Clients.API.Models;
using Ecommerce.Core.Messages;

namespace Ecommerce.Clients.API.Application.Commands
{
    public class ClienteCommandHandler : CommandHandler,
        IRequestHandler<RegistrarClienteCommand, ValidationResult>,
        IRequestHandler<AdicionarEnderecoCommand, ValidationResult>,
        IRequestHandler<AtualizarEnderecoCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(AtualizarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var temEndereco = await _clienteRepository.ObterEnderecoPorId(message.ClienteId);

            if (temEndereco == null)
            {
                AdicionarErro("Não existe endereço a ser atualizado.");
                return ValidationResult;
            }




            var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.Cep, message.Cidade, message.Estado, message.ClienteId, temEndereco.Id);
            _clienteRepository.UpdateEndereco(endereco);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }


        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf);

            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(AdicionarEnderecoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var temEndereco = await _clienteRepository.ObterEnderecoPorId(message.ClienteId);

            if (temEndereco != null)
            {
                AdicionarErro("Somente permitido um endereço.");
                return ValidationResult;
            }

            var endereco = new Endereco(message.Logradouro, message.Numero, message.Complemento, message.Bairro, message.Cep, message.Cidade, message.Estado, message.ClienteId);
            _clienteRepository.AdicionarEndereco(endereco);

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}