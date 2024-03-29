﻿using Common.Enums;
using System.Collections.Generic;

namespace Common.Models.Datatable
{
    public class DtParameters
    {
        public int Draw { get; set; }

        public DtColumn[] Columns { get; set; }

        public DtOrder[] Order { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public DtSearch Search { get; set; }

        public string SortOrder => Columns != null && Order != null && Order.Length > 0 ? (Columns[Order[0].Column].Data + (Order[0].Dir == DtOrderDir.Desc ? " " + Order[0].Dir : string.Empty)) : null;

        public IEnumerable<string> AdditionalValues { get; set; }
    }
}
