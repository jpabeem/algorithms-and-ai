using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.fuzzy_logic.terms
{
    public class FzOR : FuzzyTerm
    {
        public FzOR(FuzzyTerm op1, FuzzyTerm op2)
        {
            Terms.Add(op1.GetClone());
            Terms.Add(op2.GetClone());
        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            Terms.Add(op1.GetClone());
            Terms.Add(op2.GetClone());
            Terms.Add(op3.GetClone());
        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
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
            throw new NotImplementedException();
        }

        public override double GetDOM()
        {
            double largest = double.MinValue;

            foreach (var term in Terms)
            {
                if (term.GetDOM() > largest)
                {
                    largest = term.GetDOM();
                }
            }

            return largest;
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
