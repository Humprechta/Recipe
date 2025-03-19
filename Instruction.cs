using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe
{
    public class Instruction : IPrintable
    {
        private string _step;
        private int _duration;

        public Instruction(string step, int duration)
        {
            Step = step;
            Duration = duration;
        }
        public Instruction() {}

        public string Step
        {
            get { return _step; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new CustomException("Step cannot be empty");
                }
                _step = value;
            }
        }

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (value < 0)
                {
                    throw new CustomException("Duration must be non-negative");
                }
                _duration = value;
            }
        }

        public string GetPrintableString()
        {
            return $"{_step} - {_duration} min";
        }
    }
}
