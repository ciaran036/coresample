using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Web.ViewComponents
{
    public class ProductName : ViewComponent
    {
        private readonly DataContext _context;

        public ProductName(DataContext context)
        {
            _context = context;
        }

        public async Task<HtmlString> InvokeAsync()
        {
            var result = await _context.Settings;
            return new HtmlString(result.ProductName);
        }
    }
}
