using Ecommerce.DAL.Abstract;
using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Concrete.EntityFramework
{
    public class OrderDAL:IOrderDAL
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        public List<OrderDetail> GetAll()
        {
            return _context.OrderDetail.ToList();
        }
        public OrderDetail Find(int? id)
        {
            return _context.OrderDetail.Find(id);
        }
    }
}
