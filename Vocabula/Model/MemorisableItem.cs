using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.LearnableItems;

namespace Vocabula.Model
{    
    public class MemorisableItem
    {
        public MemorisableItem(IToBeLearnedItem toBeLearnedItem, WordStatistics stats)
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

        public WordStatistics statistics;

        private IToBeLearnedItem _toBeLearned;

        //avg 10, asked 2: sum/n = avg sum = avg * n, updated 
    }
}
