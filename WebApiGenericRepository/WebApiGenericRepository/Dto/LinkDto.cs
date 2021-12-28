using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGenericRepository.Dto
{
    public class LinkDTO
    {
        public string UserName { get; set; }
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public LinkDTO(string _href, string _rel, string _method)
        {
            Href = _href;
            Rel = _rel;
            Method = _method;
        }
    }
}
