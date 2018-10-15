namespace SteeringCS.util.fuzzy_logic
{
    /// <summary>
    /// Proxy class for a fuzzy set. The proxy class inherits from
    /// FuzzyTerm and therefore can be used to create fuzzy rules.
    /// </summary>
    public class FzSet : FuzzyTerm
    {
        public FuzzySet Set { get; private set; }

        public FzSet(FuzzySet set)
        {
            Set = set;
        }

        public override FuzzyTerm GetClone()
        {
            return new FzSet(Set);
        }

        public override double GetDOM()
        {
            return Set.DegreeOfMembership;
        }

        public override void ClearDOM()
        {
            Set.ClearDOM();
        }

        public override void ORwithDOM(double val)
        {
            Set.ORwithDOM(val);
        }
    }
}