using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic.set
{
    public class FuzzyTriangle : FuzzySet
    {
        public FuzzyTriangle(double mid, double left, double right)
            : base(mid)
        {
            PeakPoint = mid;
            LeftOffset = left;
            RightOffset = right;
        }

        public override double CalculateDom(double val)
        {
            // prevent division by zero errors
            if ((LeftOffset == 0.0 && PeakPoint == val) || (RightOffset == 0.0 && PeakPoint == val))
                return 1.0;
            //find DOM if left of center
            else if ((val <= PeakPoint) && (val >= (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center
            else if ((val > PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (val - PeakPoint) + 1.0;
            }
            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }

    }
}
