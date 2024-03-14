using System;

namespace Ecommerce.Clients.API.ViewModels
{
    public class BaseEnderecoViewModel
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }

    public class AdicionarEnderecoViewModel : BaseEnderecoViewModel
    {

    }

    public class UpdateEnderecoViewModel : BaseEnderecoViewModel
    {

    }

    public class EnderecoViewModel : BaseEnderecoViewModel
    {
        public Guid EnderecoId { get; set; }
    }
}
