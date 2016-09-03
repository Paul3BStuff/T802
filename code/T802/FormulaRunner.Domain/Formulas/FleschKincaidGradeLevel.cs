namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// Flesch Kincaid Grade Level
    /// </summary>
    public class FleschKincaidGradeLevel : IFormula
    {
        public string Name
        {
            get
            {
                return "FKGL";
            }
        }

        public double Evaluate(string text)
        {
            var formula = TextStatistics.Net.TextStatistics.Parse(text);
            return formula.FleschKincaidGradeLevel();
        }
    }
}
