using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic
{
    public enum DefuzzifyType { MAX_AV, CENTROID };

    public class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> VariableMap { get; set; }
        private List<FuzzyRule> Rules { get; set; }
        public DefuzzifyType Type { get; set; }
        private const int NumOfSamplesCentroid = 15;

        public FuzzyModule()
        {
            Type = DefuzzifyType.MAX_AV;
            VariableMap = new Dictionary<string, FuzzyVariable>();
            Rules = new List<FuzzyRule>();
        }

        /*
         * Zeros the DOMs of the consequents of each rule. Used by Defuzzify.
         */
        private void SetConfidencesOfConsequentsToZero()
        {
            foreach(var rule in Rules)
            {
                rule.SetConfidenceOfConsequentToZero();
            }
        }

        public FuzzyVariable CreateFLV(string flvName)
        {
            VariableMap[flvName] = new FuzzyVariable();
            return VariableMap[flvName];
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string flvName, double val)
        {
            if(VariableMap.ContainsKey(flvName))
            {
                VariableMap[flvName].Fuzzify(val);
            }
            else
            {
                throw new ArgumentNullException("Given FLV name does not exist!");
            }
        }

        public double DeFuzzify(string flvName, DefuzzifyType method)
        {
            if (VariableMap.ContainsKey(flvName))
            {
                SetConfidencesOfConsequentsToZero();
                foreach(var rule in Rules)
                {
                    rule.Calculate();
                }

                double returnValue;
                switch(Type)
                {
                    case DefuzzifyType.MAX_AV:
                        returnValue = VariableMap[flvName].DeFuzzifyMaxAv();
                        break;
                    default:
                        returnValue = 0;
                        break;
                }
                return returnValue;
            }
            else
            {
                throw new ArgumentNullException("Given FLV name does not exist!");
            }
        }

    }
}
