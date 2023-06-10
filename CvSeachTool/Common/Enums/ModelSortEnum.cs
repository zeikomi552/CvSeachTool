using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvSeachTool.Common.Enums
{
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
}
