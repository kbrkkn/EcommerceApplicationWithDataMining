using Ecommerce.DAL.Abstract;
using Ecommerce.DAL.Concrete.EntityFramework;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.BLL.Concrete
{
    public class OrderManager
    {
        private IOrderDAL _orderDAL = new OrderDAL();
        public List<OrderDetail> GetAll() { return _orderDAL.GetAll(); }

        public OrderDetail Find(int? id) { return _orderDAL.Find(id); }
    }
}
