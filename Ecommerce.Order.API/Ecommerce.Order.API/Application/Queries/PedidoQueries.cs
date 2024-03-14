using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Ecommerce.Order.API.Application.DTO;
using Ecommerce.Order.Domain.Pedidos;

namespace Ecommerce.Order.API.Application.Queries
{
    public interface IPedidoQueries
    {
        Task<PedidoDTO> ObterUltimoPedido(Guid clienteId);
        Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId(Guid clienteId);
        Task<PedidoDTO> ObterPedidosAutorizados();
    }

    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<PedidoDTO> ObterUltimoPedido(Guid clienteId)
        {
            //const string sql = @"SELECT
            //                    P.ID AS 'ProdutoId', P.CODIGO, P.VOUCHERUTILIZADO, P.DESCONTO, P.VALORTOTAL,P.PEDIDOSTATUS,
            //                    P.LOGRADOURO,P.NUMERO, P.BAIRRO, P.CEP, P.COMPLEMENTO, P.CIDADE, P.ESTADO,
            //                    PIT.ID AS 'ProdutoItemId',PIT.PRODUTONOME, PIT.QUANTIDADE, PIT.PRODUTOIMAGEM, PIT.VALORUNITARIO 
            //                    FROM PEDIDOS P 
            //                    INNER JOIN PEDIDOITEMS PIT ON P.ID = PIT.PEDIDOID 
            //                    WHERE P.CLIENTEID = @clienteId 
            //                    AND P.DATACADASTRO between DATEADD(minute, -3,  GETDATE()) and DATEADD(minute, 0,  GETDATE())
            //                    AND P.PEDIDOSTATUS = 1 
            //                    ORDER BY P.DATACADASTRO DESC";


            const string sql = @"SELECT
                            P.""Id"" AS ""ProdutoId"",
                            P.""Codigo"",
                            P.""VoucherUtilizado"",
                            P.""Desconto"",
                            P.""ValorTotal"",
                            P.""PedidoStatus"",
                            P.""Logradouro"",
                            P.""Numero"",
                            P.""Bairro"",
                            P.""Cep"",
                            P.""Complemento"",
                            P.""Cidade"",
                            P.""Estado"",
                            PIT.""Id"" AS ""ProdutoItemId"",
                            PIT.""ProdutoNome"",
                            PIT.""Quantidade"",
                            PIT.""ProdutoImagem"",
                            PIT.""ValorUnitario""
                        FROM
                            ""Pedidos"" P
                        INNER JOIN
                            ""PedidoItems"" PIT ON P.""Id"" = PIT.""PedidoId""
                        WHERE
                            P.""ClienteId"" = @clienteId
                            AND P.""DataCadastro"" BETWEEN NOW() - INTERVAL '3 minutes' AND NOW()
                            AND P.""PedidoStatus"" = 1
                        ORDER BY
                            P.""DataCadastro"" DESC;";

            var pedido = await _pedidoRepository.ObterConexao()
                .QueryAsync<dynamic>(sql, new { clienteId });

            return MapearPedido(pedido);
        }

        public async Task<IEnumerable<PedidoDTO>> ObterListaPorClienteId(Guid clienteId)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteId(clienteId);

            return pedidos.Select(PedidoDTO.ParaPedidoDTO);
        }

        public async Task<PedidoDTO> ObterPedidosAutorizados()
        {
            // Correção para pegar todos os itens do pedido e ordernar pelo pedido mais antigo
            //const string sql = @"SELECT 
            //                    P.ID as 'PedidoId', P.ID, P.CLIENTEID, 
            //                    PI.ID as 'PedidoItemId', PI.ID, PI.PRODUTOID, PI.QUANTIDADE 
            //                    FROM PEDIDOS P 
            //                    INNER JOIN PEDIDOITEMS PI ON P.ID = PI.PEDIDOID 
            //                    WHERE P.PEDIDOSTATUS = 1                                
            //                    ORDER BY P.DATACADASTRO";

            const string sql = @"SELECT 
    P.""Id"" as ""PedidoId"", 
    P.""Id"", 
    P.""ClienteId"", 
    PI.""Id"" as ""PedidoItemId"", 
    PI.""Id"", 
    PI.""ProdutoId"", 
    PI.""Quantidade"" 
FROM 
    ""Pedidos"" P 
INNER JOIN 
    ""PedidoItems"" PI ON P.""Id"" = PI.""PedidoId"" 
WHERE 
    P.""PedidoStatus"" = 1                                
ORDER BY 
    P.""DataCadastro"" DESC;";

            // Utilizacao do lookup para manter o estado a cada ciclo de registro retornado
            var lookup = new Dictionary<Guid, PedidoDTO>();

            await _pedidoRepository.ObterConexao().QueryAsync<PedidoDTO, PedidoItemDTO, PedidoDTO>(sql,
                (p, pi) =>
                {
                    if (!lookup.TryGetValue(p.Id, out var pedidoDTO))
                        lookup.Add(p.Id, pedidoDTO = p);

                    pedidoDTO.PedidoItems ??= new List<PedidoItemDTO>();
                    pedidoDTO.PedidoItems.Add(pi);

                    return pedidoDTO;

                }, splitOn: "PedidoId,PedidoItemId");

            // Obtendo dados o lookup
            return lookup.Values.OrderBy(p=>p.Data).FirstOrDefault();
        }

        private PedidoDTO MapearPedido(dynamic result)
        {
            var pedido = new PedidoDTO
            {
                Codigo = result[0].CODIGO,
                Status = result[0].PEDIDOSTATUS,
                ValorTotal = result[0].VALORTOTAL,
                Desconto = result[0].DESCONTO,
                VoucherUtilizado = result[0].VOUCHERUTILIZADO,

                PedidoItems = new List<PedidoItemDTO>(),
                Endereco = new EnderecoDTO
                {
                    Logradouro = result[0].LOGRADOURO,
                    Bairro = result[0].BAIRRO,
                    Cep = result[0].CEP,
                    Cidade = result[0].CIDADE,
                    Complemento = result[0].COMPLEMENTO,
                    Estado = result[0].ESTADO,
                    Numero = result[0].NUMERO
                }
            };

            foreach (var item in result)
            {
                var pedidoItem = new PedidoItemDTO
                {
                    Nome = item.PRODUTONOME,
                    Valor = item.VALORUNITARIO,
                    Quantidade = item.QUANTIDADE,
                    Imagem = item.PRODUTOIMAGEM
                };

                pedido.PedidoItems.Add(pedidoItem);
            }

            return pedido;
        }
    }

}