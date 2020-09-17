using System;
using System.Collections.Generic;
using System.Text;

namespace AdFormTodoApi.Core.Models
{
    public class SearchFilter : PagingOptions
    {
        public string ItemName { get; set; }
        
    }
}
