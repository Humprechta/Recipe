using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class Instruction : IPrintable
    {
        private string step;
        private int duration;

        public Instruction(string step, int duration)
        {
            Step = step;
            Duration = duration;
        }
        public Instruction() {}

        public string Step
        {
            get { return step; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomException("Step cannot be empty");
                }
                step = value;
            }
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                if (value < 0)
                {
                    throw new CustomException("Duration must be non-negative");
                }
                duration = value;
            }
        }

        public string GetPrintableString()
        {
            return $"{step} - {duration} min";
        }
    }
}
