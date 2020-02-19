using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {

            int weekendsCount = 0;
            DateTime estimatedEndDate = startDate.AddDays(dayCount - 1);

            if (weekEnds.IsNullOrEmpty())
            {
                return estimatedEndDate;
            }

            foreach (WeekEnd item in weekEnds)
            {
                if (item.EndDate<startDate)
                {
                    continue;
                }
            }

            return estimatedEndDate.AddDays(weekendsCount == 0 ? 0 : weekendsCount + 1);
        }

    }
}
