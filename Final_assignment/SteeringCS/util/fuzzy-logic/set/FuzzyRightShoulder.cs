using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic
{
    public class FuzzyRightShoulder : FuzzySet
    {
        public FuzzyRightShoulder(double peak, double left, double right) 
            : base(((peak + right) + peak) / 2)
        {
            PeakPoint = peak;
            LeftOffset = left;
            RightOffset = right;
        }

        public override double CalculateDom(double val)
        {
            // prevent division by zero errors
            if ((LeftOffset == 0.0 && PeakPoint == val) || (RightOffset == 0.0 && PeakPoint == val))
                return 1.0;
            //find DOM if left of center
            else if ((val <= PeakPoint) && (val > (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center and less than center + right offset
            else if ((val > PeakPoint) && (val <= PeakPoint + RightOffset))
            {
                return 1.0;
            }
            else
            {
                return 0;
            }
        }

    }
}
