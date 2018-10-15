using System;
using System.Collections.Generic;

namespace SteeringCS.util.fuzzy_logic
{
    public abstract class FuzzyTerm : ICloneable
    {
        public List<FuzzyTerm> Terms { get; set; }
        public abstract FuzzyTerm GetClone();
        public abstract double GetDOM();
        public abstract void ClearDOM();
        public abstract void ORwithDOM(double val);

        public object Clone()
        {
            return MemberwiseClone();

        }
    }
}