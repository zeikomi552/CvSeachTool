﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Common.Enums
{
    /// <summary>
    /// モデル検索用
    /// </summary>
    public enum ModelSortEnum
    {
        [Description("")]
        Empty,
        [Description("Highest Rated")]
        Highest_Rated,
        [Description("Most Downloaded")]
        Most_Downloaded,
        [Description("Newest")]
        Newest
    }

    /// <summary>
    /// Image検索用
    /// </summary>
    public enum ModelSortEnum2
    {
        [Description("")]
        Empty,
        [Description("Most Reactions")]
        Most_Reactions,
        [Description("Most Comments")]
        Most_Comments,
        [Description("Newest")]
        Newest
    }
}
