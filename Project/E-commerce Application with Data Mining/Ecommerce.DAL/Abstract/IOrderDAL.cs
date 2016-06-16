using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{
    public interface IOrderDAL
    {
        List<OrderDetail> GetAll();
        OrderDetail Find(int? id);
    }
}
