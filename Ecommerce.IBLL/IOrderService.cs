using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.IBLL
{
    public interface IOrderService
    {
        List<OrderDTO> GetAll(int pageNumber, string? name, string? userName, DateTime? dateFrom, DateTime? dateTo);
        OrderDTO GetById(int id);
        OrderDTO Create(OrderDTO order);

    }
}
