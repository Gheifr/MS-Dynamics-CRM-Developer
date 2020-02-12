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
            DateTime output;

            int weekendsCount = 0;


            if (!weekEnds.IsNullOrEmpty())
            {
                foreach (WeekEnd item in weekEnds)
                {
                    if (!(startDate.AddDays(dayCount + weekendsCount) < item.EndDate))
                    {
                        weekendsCount += (item.EndDate - item.StartDate).Days;
                    }
                    else if (startDate.AddDays(dayCount + weekendsCount) == item.EndDate)
                    {
                        if (startDate.AddDays(dayCount + weekendsCount) < item.StartDate)
                        {
                            weekendsCount += (startDate.AddDays(dayCount + weekendsCount) - item.StartDate).Days;
                        }
                    }
                }
            }
            else
            {
                return startDate.AddDays(dayCount-1);
            }

            output = startDate.AddDays(dayCount + weekendsCount);

            return output;
        }

    }
}
