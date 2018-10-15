using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic
{
    public abstract class FuzzySet
    {
        public double PeakPoint { get; set; }
        public double LeftOffset { get; set; }
        public double RightOffset { get; set; }

        /// <summary>
        /// Holds the degree of membership in this set.
        /// </summary>
        public double DegreeOfMembership { get; set; }

        /// <summary>
        /// This is the maximum of the set's membership function.
        /// E.g., if the set is triangular then this will be the peak point
        /// of the triangle. If the set has a plateau, then this value will be the midpoint of 
        /// the plateau. This value is set in the constructor to avoid run-time calculation
        /// of midpoint values.
        /// </summary>
        public double RepresentativeValue { get; set; }

        public FuzzySet(double representativeValue)
        {
            DegreeOfMembership = 0.0;
            RepresentativeValue = representativeValue;
        }

        /// <summary>
        /// Return the degree of membership in this set of the given value.
        /// This does NOT set DegreeOfMembership of the value passed as the parameter.
        /// This is because the centroid defuzzification method also uses this method
        /// to determine the DOMs of the values it uses as its sample points.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public abstract double CalculateDom(double val);

        /// <summary>
        /// If this fuzzy set is part of a consequent FLV, and it is fired by a rule,
        /// then this method sets the DOM (in this context, the DOM represents a
        /// confidence level) to the maximum of the parameter value or the set's 
        /// existing DOM value).
        /// </summary>
        /// <param name="val"></param>
        public void ORwithDOM(double val)
        {
            if (val > DegreeOfMembership)
                DegreeOfMembership = val;
        }

        public void ClearDOM()
        {
            DegreeOfMembership = 0.0;
        }

        public void SetDOM(double val) { DegreeOfMembership = val; }

    }
}
