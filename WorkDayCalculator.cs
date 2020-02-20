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
                    for (int i = 0; i <= (item.EndDate - item.StartDate).Days; i++)
                    {
                        actualWeekEnds.Add(item.StartDate.AddDays(i));
                    }
                }
                else
                {
                    actualWeekEnds.Add(item.StartDate);
                }
            }

            actualWeekEnds = actualWeekEnds
                            .Distinct()
                            .Where(x => x >= startDate)
                            .ToList();

            actualWeekEnds.Sort();

            foreach (DateTime dt in actualWeekEnds)
            {
                if (dt <= estimatedEndDate)
                {
                    estimatedEndDate = estimatedEndDate.AddDays(1);
                }
                else if (dt > estimatedEndDate)
                {
                    break;
                }
            }

            return estimatedEndDate;
        }

    }
}
