using System;

namespace ProAndCate.Models
{
    public class ErrorViewModel
    {
        public string RequstId { get; set; }
        public bool ShowRequestedId => ! string.IsNullOrEmpty(RequstId);
    }
}
