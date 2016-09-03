using System;
using ts = TextStatistics.Net;

namespace FormulaRunner.Domain.Formulas
{
    /// <summary>
    /// Gunning Fog index
    /// </summary>
    public class GunningFog : IFormula
    {
        private ts.TextStatistics _formula;
        public string Name => "GF";

        public double Evaluate(string text)
        {
            _formula = ts.TextStatistics.Parse(text);
            return _formula.GunningFogScore();
        }
    }
}
