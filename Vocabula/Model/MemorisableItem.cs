using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.LearnableItems;

namespace Vocabula.Model
{    
    public class Statistics
    {
        public DateTime DateLearned;
        public DateTime LastTimeAnswered;
        public int NumberOfTimesAsked;
        public int NumberOfTimesAnsweredCorrectly;
        public string UniqueId;
    }

    public class MemorisableItem
    {
        public MemorisableItem(IToBeLearnedItem toBeLearnedItem, Statistics stats)
        {
            _toBeLearned = toBeLearnedItem;
            statistics = stats;
        }

        public string GetTheAnswer()
        {
            return _toBeLearned.GetExpectedResult();
        }

        public string GetTheQuestion()
        {
            return _toBeLearned.GetQuestion();
        }

        public bool CheckAnswer(string submittedAnswer)
        {
            return GetTheAnswer().Equals(submittedAnswer);
        }

        public IToBeLearnedItem GetLearnableItem()
        {
            return _toBeLearned;
        }

        public Statistics statistics;

        private IToBeLearnedItem _toBeLearned;

        //avg 10, asked 2: sum/n = avg sum = avg * n, updated 
    }
}
