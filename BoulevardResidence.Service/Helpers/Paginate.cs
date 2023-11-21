using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoulevardResidence.Service.Helpers
{
    public class Paginate<T>
    {
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public Paginate(List<T> datas, int currentPage, int totalPage, string langCode)
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
            LangCode = langCode;
        }
        public bool HasPrevious
        {
            get
            {
                return CurrentPage > 1;
            }
        }
        public bool HasNext
        {
            get
            {
                return CurrentPage < TotalPage;
            }
        }

        public string LangCode { get; set; }
    }
}
