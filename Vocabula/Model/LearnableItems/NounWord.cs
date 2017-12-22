using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model.LearnableItems
{
    public class NounWord : IToBeLearnedItem
    {
        private GenderEnum _nounGender;

        private string _questionWord;

        private string _answerWord;

        private IGenderContext _context;

        public NounWord(GenderEnum nounGender, string questionWord, string answerWord, IGenderContext genderContext)
        {
            _context = genderContext;
            _nounGender = nounGender;
            _questionWord = questionWord;
            _answerWord = answerWord;
        }

        public string GetExpectedResult()
        {
            return _context.GetAnswerGender(_nounGender) + " " + _answerWord;
        }

        public string GetQuestion()
        {
            return _context.GetQuestionGender(_nounGender) + " " + _questionWord;
        }
    }
}
