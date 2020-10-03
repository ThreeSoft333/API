using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeSoftECommAPI.Contracts.V1.Responses.Charts
{
    public class DrillDownChartCategory
    {
        public List<CategoryPcntChart> categoryPcntCharts { get; set; }
        public List<SubCategoryPcntChart> subCategoryPcntCharts { get; set; }
    }
}
