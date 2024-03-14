using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Core.Data;

namespace Ecommerce.Clients.API.Models
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);

        Task<IEnumerable<Cliente>> ObterTodos();
        Task<Cliente> ObterPorCpf(string cpf);

        void AdicionarEndereco(Endereco endereco);
        Task<Endereco> ObterEnderecoPorId(Guid id);


        //Task<bool> RemoverEndereco(Guid id);

        void UpdateEndereco(Endereco endereco);
      
    }
}