using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculatorException : Exception
    {
        public WorkDayCalculatorException() : base(){}
        public WorkDayCalculatorException(string message) : base(message){}
        public WorkDayCalculatorException(string message, Exception inner) : base(message, inner){}
    }
}
