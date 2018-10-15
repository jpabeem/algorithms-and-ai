using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic
{
    public class FzAND : FuzzyTerm, ICloneable
    {
        public FzAND()
        {
            Terms = new List<FuzzyTerm>();
        }
        public FzAND(FuzzyTerm op1, FuzzyTerm op2)
        {
            Terms = new List<FuzzyTerm>();
            Terms.Add(op1.GetClone());
            Terms.Add(op2.GetClone());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            Terms = new List<FuzzyTerm>();
            Terms.Add(op1.GetClone());
            Terms.Add(op2.GetClone());
            Terms.Add(op3.GetClone());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            Terms = new List<FuzzyTerm>();
            Terms.Add(op1.GetClone());
            Terms.Add(op2.GetClone());
            Terms.Add(op3.GetClone());
            Terms.Add(op4.GetClone());
        }

        public override void ClearDOM()
        {
            foreach (var term in Terms)
            {
                term.ClearDOM();
            }
        }

        public override FuzzyTerm GetClone()
        {
            return (FuzzyTerm)MemberwiseClone();
        }

        public override double GetDOM()
        {
            double smallest = double.MaxValue;

            foreach (var term in Terms)
            {
                if (term.GetDOM() < smallest)
                {
                    smallest = term.GetDOM();
                }
            }

            return smallest;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override void ORwithDOM(double val)
        {
            foreach (var term in Terms)
            {
                term.ORwithDOM(val);
            }

        }
    }
}
