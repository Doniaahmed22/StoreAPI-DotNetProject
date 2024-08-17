using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Reposatry.Specification.Product
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

        public int PageIndex { get; set; } = 1;
        public  const int MAXPAGESIZE = 50;
        private int _pageSize=6;

        public int PageSize
        {
            get { return _pageSize; }
            set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
        }

        private string? _search;

        public string? Search
        {
            get => _search;
            set => _search = value?.Trim().ToLower();
        }

    }
}
