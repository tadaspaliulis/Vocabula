using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.LearnableItems
{
    class AdjectiveWord : LearnedItemBase
    {
        public AdjectiveWord(string questionWord, string answerWord)
            : base(questionWord, answerWord)
        {
        }

        public override string GetExpectedResult()
        {
            return AnswerWord;
        }

        public override string GetQuestion()
        {
            return QuestionWord;
        }
    }
}
