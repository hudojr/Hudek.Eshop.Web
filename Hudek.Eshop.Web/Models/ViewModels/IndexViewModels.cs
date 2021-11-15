using Hudek.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hudek.Eshop.Web.Models.ViewModels
{
    public class IndexViewModels
    {
        public IList<CarouselItem> CarouselItems { get; set; }
        public IList<ProductItem> ProductItems { get; set; }


    }
}
