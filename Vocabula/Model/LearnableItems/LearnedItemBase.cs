using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.LearnableItems
{
    public abstract class LearnedItemBase : IToBeLearnedItem
    {
        protected LearnedItemBase(string questionWord, string answerWord)
        {
            QuestionWord = questionWord;
            AnswerWord = answerWord;
        }

        public string QuestionWord { get; private set; }
        public string AnswerWord { get; private set; }
        public string UniqueId
        {
            get
            {
                //What if the representation for this changes (even capitalization)? should generate something else instead for unique ids
                return GetQuestion();
            }
        }
        public abstract string GetExpectedResult();
        public abstract string GetQuestion();
    }
}
