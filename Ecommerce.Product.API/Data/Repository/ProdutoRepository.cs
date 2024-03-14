using Ecommerce.Product.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Core.Data;

namespace Ecommerce.Product.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PagedResult<Produto>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT ""Id"", ""Nome"", ""Descricao"", ""Ativo"", ""Valor"", ""DataCadastro"", ""Imagem"", ""QuantidadeEstoque"" FROM ""Produtos"" 
                 WHERE (@Nome IS NULL OR Lower(""Nome"") LIKE '%' || Lower(@Nome) || '%') 
                 ORDER BY ""Nome"" 
                 OFFSET @Offset
                 LIMIT @PageSize;
                
                 SELECT COUNT(1) FROM ""Produtos"" WHERE (@Nome IS NULL OR ""Nome"" LIKE '%' || @Nome || '%')";



            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query, Offset = pageIndex, PageSize = pageSize });

            var produtos = multi.Read<Produto>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Produto>()
            {
                List = produtos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<List<Produto>> ObterProdutosPorId(string ids)
        {
            var idsGuid = ids.Split(',')
                .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.Ok)) return new List<Produto>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Produtos.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Ativo).ToListAsync();
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}