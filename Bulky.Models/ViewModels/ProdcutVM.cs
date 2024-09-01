using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public   class ProdcutVM
    {
        public  Product  Prodcut { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem>  CategoryList { get; set; }
    }
}
