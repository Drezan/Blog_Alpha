using System;
using System.Collections.Generic;
using System.Text;

namespace Blog_Alpha.Models
{
    public class Records
    {
        public Nullable<int> Created_By { get; set; }
        public Nullable<int> Modified_By { get; set; }
        public Nullable<DateTime> Created_At { get; set; }
        public Nullable<DateTime> Modified_At { get; set; }
    }
}
