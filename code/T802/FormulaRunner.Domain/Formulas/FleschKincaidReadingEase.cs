using stats = TextStatistics.Net;

namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// Flesch-Kincaid Reading Ease
    /// </summary>
    public class FleschKincaidReadingEase : IFormula
    {
        public string Name
        {
            get
            {
                return "FKRE";
            }
        }

        public double Evaluate(string text)
        {
            var formula = stats.TextStatistics.Parse(text);
            return formula.FleschKincaidReadingEase();
        }
    }
}
