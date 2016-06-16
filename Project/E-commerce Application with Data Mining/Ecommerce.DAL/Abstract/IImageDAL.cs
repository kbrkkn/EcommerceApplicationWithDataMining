using Ecommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Abstract
{
    public interface IImageDAL
    {
        void Add(Image[] im);
        void Add(Image im);
    }
}
