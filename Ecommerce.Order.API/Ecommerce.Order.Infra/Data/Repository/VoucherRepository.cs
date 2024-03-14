using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Core.Data;
using Ecommerce.Order.Domain;

namespace Ecommerce.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly PedidosContext _context;

        public VoucherRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public void Atualizar(Voucher voucher)
        {
            _context.Vouchers.Update(voucher);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}