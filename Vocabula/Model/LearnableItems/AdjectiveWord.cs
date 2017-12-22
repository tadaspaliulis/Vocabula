using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.LearnableItems
{
    class AdjectiveWord : IToBeLearnedItem
    {
        private string _question;
        private string _answer;

        public AdjectiveWord(string question, string answer)
        {
            _question = question;
            _answer = answer;
        }

        public string GetExpectedResult()
        {
            return _answer;
        }

        public string GetQuestion()
        {
            return _question;
        }
    }
}
