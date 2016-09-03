namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// Coleman-Laiu index
    /// </summary>
    public class ColemanLiau : IFormula
    {
        public string Name
        {
            get
            {
                return "CL";
            }
        }

        public double Evaluate(string text)
        {
            var formula = TextStatistics.Net.TextStatistics.Parse(text);
            return formula.ColemanLiauIndex();
        }
    }
}
