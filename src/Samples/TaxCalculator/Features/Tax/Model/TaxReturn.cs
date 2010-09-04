using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaxCalculator.Features.Tax.Model
{
    public class TaxEstimate
    {
        private readonly Situation _situation;

        public TaxEstimate(Situation situation)
        {
            _situation = situation;
        }

        public Situation Situation
        {
            get { return _situation; }
        }

        public decimal TaxOnIncome { get; set; }
        
        public decimal MedicareLevy { get; set; }
        
        public decimal NetIncome
        {
            get { return Situation.GrossIncome - (TaxOnIncome + MedicareLevy); }
        }
    }

    public interface ITaxEstimator
    {
        TaxEstimate Estimate(Situation situation);
    }

    public interface ITaxEstimatorSelector
    {
        ITaxEstimator Select(TaxPeriod period);
    }

    public class TaxEstimatorSelector : ITaxEstimatorSelector
    {
        private readonly Dictionary<TaxPeriod, ITaxEstimator> _estimatorsByPeriod = new Dictionary<TaxPeriod, ITaxEstimator>();

        public void AddTaxRate(TaxPeriod period, ITaxEstimator estimator)
        {
            _estimatorsByPeriod.Add(period, estimator);
        }

        public ITaxEstimator Select(TaxPeriod period)
        {
            return _estimatorsByPeriod[period];
        }
    }

    public class TaxEstimator : ITaxEstimator
    {
        private readonly TaxBracketSelector _bracketSelector;
        private readonly ITaxModifier[] _modifiers;

        public TaxEstimator(TaxBracketSelector bracketSelector, params ITaxModifier[] modifiers)
        {
            _bracketSelector = bracketSelector;
            _modifiers = modifiers;
        }

        public TaxEstimate Estimate(Situation situation)
        {
            var estimate = new TaxEstimate(situation);
            var bracket = _bracketSelector.SelectBracket(situation);
            estimate.TaxOnIncome = bracket.CalculateTaxPayable(situation);

            foreach (var modifier in _modifiers)
            {
                modifier.Apply(estimate);
            }

            return estimate;
        }
    }

    public interface ITaxModifier
    {
        void Apply(TaxEstimate estimate);
    }

    public class MedicareLevy : ITaxModifier
    {
        private readonly decimal _levyFreeThreshold;
        private readonly decimal _levyRate;

        public MedicareLevy(decimal levyFreeThreshold, decimal levyRate)
        {
            _levyFreeThreshold = levyFreeThreshold;
            _levyRate = levyRate;
        }

        public void Apply(TaxEstimate estimate)
        {
            if (estimate.Situation.GrossIncome < _levyFreeThreshold)
                return;

            estimate.MedicareLevy = (estimate.Situation.GrossIncome)*_levyRate;
        }
    }

    public class Situation
    {
        private readonly decimal _grossIncome;

        public Situation(decimal grossIncome)
        {
            _grossIncome = grossIncome;
        }

        public decimal GrossIncome
        {
            get { return _grossIncome; }
        }
    }

    public class TaxBracketSelector
    {
        private readonly TaxBracket[] _brackets;

        public TaxBracketSelector(params TaxBracket[] brackets)
        {
            _brackets = brackets;
        }

        public TaxBracket SelectBracket(Situation situation)
        {
            return _brackets.Single(x => x.IsWithinBracket(situation));
        }
    }

    public class TaxBracket
    {
        private readonly decimal _from;
        private readonly decimal _to;
        private readonly decimal _flagfall;
        private readonly decimal _rate;

        public TaxBracket(decimal from, decimal to, decimal flagfall, decimal rate)
        {
            _from = from;
            _to = to;
            _flagfall = flagfall;
            _rate = rate;
        }

        public bool IsWithinBracket(Situation situation)
        {
            return _from < situation.GrossIncome && situation.GrossIncome <= _to;
        }

        public decimal CalculateTaxPayable(Situation situation)
        {
            return _flagfall + ((situation.GrossIncome - _from) *_rate);
        }
    }
}
