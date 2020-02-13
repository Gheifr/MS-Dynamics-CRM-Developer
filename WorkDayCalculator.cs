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

            // check that weekends exists and not empty
            if (!weekEnds.IsNullOrEmpty())
            {
                foreach (WeekEnd item in weekEnds)
                {

                    // case when start date is within weekends range
                    if (startDate >= item.StartDate && startDate <= item.EndDate)//)
                    {
                        if (!(item.StartDate == item.EndDate))
                        {
                            weekendsCount += (int)(item.EndDate - startDate).Days;
                        }
                        else
                        {
                            weekendsCount += 1;
                        }
                    }

                    // start date should not be greater than weekends end date or 
                    //weekend start should not be greater than start day plus already added days
                    else if (startDate > item.EndDate || startDate.AddDays(dayCount + weekendsCount) < item.StartDate)
                    {
                        continue;
                    }

                    // case when weekend start date is within start day plus already added days
                    else if (startDate.AddDays(dayCount + weekendsCount) >= item.StartDate)
                    {
                        if (!(item.StartDate == item.EndDate))
                        {
                            weekendsCount += (int)(item.EndDate - item.StartDate).Days;
                        }
                    }
                }
            }
            else
            {
                return startDate.AddDays(dayCount - 1);
            }

            return startDate.AddDays(dayCount + weekendsCount);
        }

    }
}
