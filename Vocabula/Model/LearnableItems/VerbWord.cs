using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model.LearnableItems
{
    public class VerbWord : LearnedItemBase
    {
        public PersonalPronounEnum PersonalPronoun { get; private set; }

        private IPronounContext _context;

        public VerbWord(PersonalPronounEnum pronoun, string questionWord, string answerWord, IPronounContext pronounContext)
            :base (questionWord, answerWord)
        {
            _context = pronounContext;
            PersonalPronoun = pronoun;
        }

        public override string GetExpectedResult()
        {
            return _context.GetAnswerPersonalPronoun(PersonalPronoun) + " " + AnswerWord;
        }

        public override string GetQuestion()
        {
            return _context.GetQuestionPersonalPronoun(PersonalPronoun) + " " + QuestionWord;
        }
    }
}
