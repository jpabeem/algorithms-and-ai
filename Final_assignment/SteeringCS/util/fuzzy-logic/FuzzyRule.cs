namespace SteeringCS.util.fuzzy_logic
{
    public class FuzzyRule
    {
        // can be a composite of several fuzzy sets and operators
        public FuzzyTerm Antecedent { get; private set; }

        public FuzzyTerm Consequent { get; private set; }

        public FuzzyRule(FuzzyTerm antecedent, FuzzyTerm consequent)
        {
            Antecedent = antecedent.GetClone();
            Consequent = consequent.GetClone();
        }
        
        public void SetConfidenceOfConsequentToZero()
        {
            Consequent.ClearDOM();
        }

        /// <summary>
        /// This method updates the DOM (confidence) of the consequent term
        /// with the DOM of the antecedent term.
        /// </summary>
        public void Calculate()
        {
            Consequent.ORwithDOM(Antecedent.GetDOM());
        }
    }
}