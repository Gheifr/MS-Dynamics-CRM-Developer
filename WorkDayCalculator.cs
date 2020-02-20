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
            int weekEndDays =0;
            List<DateTime> actualWeekEnds = new List<DateTime>();

            if (dayCount < 0)
            {
                throw new WorkDayCalculatorException("Days count is less than 0!");
            }

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

                if (item.EndDate < startDate)
                {
                    continue;
                }

                if (item.StartDate > estimatedEndDate)
                {
                    continue;
                }
                
                if (item.StartDate <= startDate && item.EndDate >= startDate)
                {
                    //estimatedEndDate = estimatedEndDate.AddDays(item.EndDate.Day - startDate.Day + 1);
                    //continue;
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
                    continue;
                }

                if (item.StartDate >= startDate && item.StartDate <= estimatedEndDate)
                {
                    //estimatedEndDate = estimatedEndDate.AddDays(item.EndDate.Day - item.StartDate.Day + 1);
                    if (item.StartDate != item.EndDate)
                    {
                        for (int i = 0; i <= item.EndDate.Day - item.StartDate.Day; i++)
                        {
                            actualWeekEnds.Add(item.StartDate.AddDays(i));
                        }
                    }
                    else
                    {
                        actualWeekEnds.Add(item.StartDate);
                    }
                }

                actualWeekEnds = actualWeekEnds.Distinct().ToList();

                weekEndDays = actualWeekEnds.Count - weekEndDays;

                estimatedEndDate.AddDays(weekEndDays);
            }

            

            return estimatedEndDate.AddDays(actualWeekEnds.Count);
        }

    }
}
