using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Clients.API.Application.Commands;
using Ecommerce.Clients.API.Models;
using Ecommerce.Core.Mediator;
using Ecommerce.WebAPI.Core.Controllers;
using Ecommerce.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.Clients.API.ViewModels;

namespace Ecommerce.Clients.API.Controllers
{
    [Authorize]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public ClientesController(IClienteRepository clienteRepository, IMediatorHandler mediator, IAspNetUser user)
        {
            _clienteRepository = clienteRepository;
            _mediator = mediator;
            _user = user;
        }

        [HttpGet("cliente/endereco")]
        public async Task<IActionResult> ObterEndereco()
        {
            var endereco = await _clienteRepository.ObterEnderecoPorId(_user.ObterUserId());

            if(endereco == null)
                return NotFound();

            var result = new EnderecoViewModel
            {
                Bairro = endereco.Bairro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Complemento = endereco.Complemento,
                EnderecoId = endereco.Id,
                Estado = endereco.Estado,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                
            };

            return CustomResponse(result);
        }

        [HttpPost("cliente/endereco")]
        public async Task<IActionResult> AdicionarEndereco(AdicionarEnderecoViewModel endereco)
        {
            var command = new AdicionarEnderecoCommand()
            {
                Bairro = endereco.Bairro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Complemento = endereco.Complemento,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
            };


            command.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediator.EnviarComando(command));
        }

        [HttpPut("client/endereco")]
        public async Task<IActionResult> AtualizarEndereco(UpdateEnderecoViewModel endereco)
        {
            var command = new AtualizarEnderecoCommand()
            {
                Bairro = endereco.Bairro,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Complemento = endereco.Complemento,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
            };

            command.ClienteId = _user.ObterUserId();
            return CustomResponse(await _mediator.EnviarComando(command));
        }


    }
}