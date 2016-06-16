using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ecommerce.Entities.ViewModel
{
  public  class AdvicedProduct
    {
      public string Name { get; set; }
      public Image advicedImage { get; set; }
      [NotMapped]
      public HttpPostedFileBase File { get; set; }

      public byte[] ImageData { get; set; }

      public string FileName { get; set; }

      public int Product_Id { get; set; }

    }
}
