using System;
namespace Resort.Types
{
    class Range
    {
        public int StartValue { get; set; }
        public int EndValue { get; set; }

        public Range(int startValue, int endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
        }

        public void Deconstruct(out int startValue, out int endValue)
        {
            startValue = StartValue;
            endValue = EndValue;
        }
    }
}
