using SteeringCS.util.fuzzy_logic.set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic
{
    public class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> MemberSets { get; set; }

        /* minimum and maximum ranges of this variable */
        private double MinRange { get; set; }
        private double MaxRange { get; set; }

        public FuzzyVariable()
        {
            MemberSets = new Dictionary<string, FuzzySet>();
            MinRange = 0.0;
            MaxRange = 0.0;
        }

        /// <summary>
        /// This method is called when the upper and lower bound of a set each time a
        /// new set is added to adjust the upper and lower range values accordingly
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void AdjustRangeToFit(double min, double max)
        {
            if (min < MinRange)
                MinRange = min;
            if (max > MaxRange)
                MaxRange = max;
        }

        public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);
            MemberSets.Add(name, new FuzzyLeftShoulder(peak, peak - minBound, maxBound - peak));
            return new FzSet(MemberSets[name]);
        }

        public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);
            MemberSets.Add(name, new FuzzyRightShoulder(peak, peak - minBound, maxBound - peak));
            return new FzSet(MemberSets[name]);
        }

        public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);
            MemberSets.Add(name, new FuzzyTriangle(peak, peak - minBound, maxBound - peak));
            return new FzSet(MemberSets[name]);
        }

        /// <summary>
        /// Not sure if this is needed?
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minBound"></param>
        /// <param name="peak"></param>
        /// <param name="maxBound"></param>
        /// <returns></returns>
        public FzSet AddSingletonSet(string name, double minBound, double peak, double maxBound)
        {
            MemberSets.Add(name, new FuzzyTriangle(peak, peak - minBound, maxBound - peak));
            AdjustRangeToFit(minBound, maxBound);
            return new FzSet(MemberSets[name]);
        }

        /// <summary>
        /// Fuzzify a value by calculating its DOM in each of this variable's subsets
        /// </summary>
        /// <param name="val"></param>
        public void Fuzzify(double val)

        {
            if ((val < MinRange) && (val <= MaxRange))
                throw new ArgumentOutOfRangeException("Fuzzy value is out of range!");

            foreach (var set in MemberSets)
            {
                set.Value.SetDOM(set.Value.CalculateDom(val));
            }
        }

        public double DeFuzzifyMaxAv()
        {
            double bottom = 0.0;
            double top = 0.0;

            foreach (var set in MemberSets)
            {
                bottom += set.Value.DegreeOfMembership;
                top += (set.Value.RepresentativeValue * set.Value.DegreeOfMembership);
            }

            if (bottom == 0) return 0.0;

            return top / bottom;
        }

        /// <summary>
        /// I decided not to implement centroid.
        /// </summary>
        /// <param name="numOfSamples"></param>
        /// <returns></returns>
        public double DeFuzzifyCentroid(int numOfSamples)
        {
            return 0.0;
        }
    }
}
