using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model
{
    public class StatisticsBasedWeightCalculator : IWeightCalculator<MemorisableItem>
    {
        private const int _minWeight = 1;
        private const int _baseWeight = 100;
        private const int _daysSinceLearnedWeight = 100;
        private const int _daysSinceAskedWeight = 10;
        private const int _incorrectAnswersRatioWeight = 1000;

        private double _averageIncorrectAnswerRatio;
        private int _count;

        public bool PreprocessingNeeded { get { return true; } }

        public int CalculateItemWeight(MemorisableItem item)
        {
            var stats = item.statistics;
            //var daysSinceLearned = (DateTime.Today - stats.DateLearned).Days;
            var daysSinceLastAsked = (DateTime.Today - stats.LastTimeAnswered).Days;

            var incorrectAnswerRatio = CalculateIncorrectAnswerRatio(stats.NumberOfTimesAnsweredCorrectly, stats.NumberOfTimesAsked);
            // Avg incorrect: 50.0
            // Cur incorrect: 40.0
            // Delta = 40.0 - 50.0 = -10.0
            var offsetFromAverage = incorrectAnswerRatio - _averageIncorrectAnswerRatio;
            
            //At the moment the weight average gives a heavy bias to the item's incorrect answer ratio compared to an overall average.
            //If you know it worse than the average word, it will much more likely to be asked
            var weight = _baseWeight + (int)(offsetFromAverage * _incorrectAnswersRatioWeight)
                + _daysSinceAskedWeight * daysSinceLastAsked;
            // + _daysSinceLearnedWeight * daysSinceLearned;

            return Math.Max(_minWeight, weight);
        }

        public void PreprocessItem(MemorisableItem item)
        {
            _count += 1;
            var stats = item.statistics;
            var incorrectAnswerRatio = CalculateIncorrectAnswerRatio(stats.NumberOfTimesAnsweredCorrectly, stats.NumberOfTimesAsked);

            _averageIncorrectAnswerRatio = (_averageIncorrectAnswerRatio * (_count - 1) + incorrectAnswerRatio) / _count;
        }

        public void Reset()
        {
            _count = 0;
            _averageIncorrectAnswerRatio = 0.0;
        }

        private double CalculateIncorrectAnswerRatio(int NumberOfTimesAnsweredCorrectly, int NumberOfTimesAsked)
        {
            return (1.0 - (double)NumberOfTimesAnsweredCorrectly / (double)NumberOfTimesAsked);
        }
    }
}
