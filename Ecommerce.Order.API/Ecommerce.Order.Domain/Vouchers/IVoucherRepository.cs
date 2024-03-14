﻿using System.Threading.Tasks;
using Ecommerce.Core.Data;

namespace Ecommerce.Order.Domain
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> ObterVoucherPorCodigo(string codigo);
        void Atualizar(Voucher voucher);
    }
}