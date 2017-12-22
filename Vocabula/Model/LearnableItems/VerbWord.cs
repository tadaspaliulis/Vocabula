using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model.LearnableItems
{
    public class VerbWord : IToBeLearnedItem
    {
        private PersonalPronounEnum _personalPronoun;

        private string _questionWord;

        private string _answerWord;

        private IPronounContext _context;

        public VerbWord(PersonalPronounEnum pronoun, string questionWord, string answerWord, IPronounContext pronounContext)
        {
            _context = pronounContext;
            _personalPronoun = pronoun;
            _questionWord = questionWord;
            _answerWord = answerWord;
        }

        public string GetExpectedResult()
        {
            return _context.GetAnswerPersonalPronoun(_personalPronoun) + " " + _answerWord;
        }

        public string GetQuestion()
        {
            return _context.GetQuestionPersonalPronoun(_personalPronoun) + " " + _questionWord;
        }
    }
}
