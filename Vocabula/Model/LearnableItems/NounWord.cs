using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model.LearnableItems
{
    public class NounWord : LearnedItemBase
    {
        public GenderEnum NounGender { get; private set; }

        private IGenderContext _context;

        public NounWord(GenderEnum nounGender, string questionWord, string answerWord, IGenderContext genderContext)
            :base(questionWord, answerWord)
        {
            _context = genderContext;
            NounGender = nounGender;
        }

        public override string GetExpectedResult()
        {
            return _context.GetAnswerGender(NounGender) + " " + AnswerWord;
        }

        public override string GetQuestion()
        {
            return _context.GetQuestionGender(NounGender) + " " + QuestionWord;
        }
    }
}
