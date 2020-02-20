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

            if (dayCount < 0)
            {
                throw new WorkDayCalculatorException("Days count is less than 0!");
            }

            List<DateTime> actualWeekEnds = new List<DateTime>();
            DateTime estimatedEndDate = startDate.AddDays(dayCount == 0 ? 0 : dayCount - 1);

            if (weekEnds.IsNullOrEmpty())
            {
                return estimatedEndDate;
            }

            foreach (WeekEnd item in weekEnds)
            {
                if (item.EndDate < item.StartDate)
                {
                    throw new WorkDayCalculatorException("Weekend period end date is less than it's start day!");
                }

                if (item.StartDate != item.EndDate)
                {
                    for (int i = 0; i <= item.EndDate.Day - startDate.Day; i++)
                    {
                        actualWeekEnds.Add(startDate.AddDays(i));
                    }
                }
                else
                {
                    actualWeekEnds.Add(startDate);
                }
            }

            

            return estimatedEndDate.AddDays(actualWeekEnds.Count);
        }

    }
}
