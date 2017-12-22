using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model
{
    public class LanguageContext : IPronounContext, IGenderContext
    {
        private Dictionary<GenderEnum, string> _answerGenderMap;
        private Dictionary<GenderEnum, string> _questionGenderMap;

        private Dictionary<PersonalPronounEnum, string> _answerPronounMap;
        private Dictionary<PersonalPronounEnum, string> _questionPronounMap;

        public LanguageContext(Dictionary<GenderEnum, string> questionGenderMap, Dictionary<GenderEnum, string> answerGenderMap,
            Dictionary<PersonalPronounEnum, string> questionPronounMap, Dictionary<PersonalPronounEnum, string> answerPronounMap)
        {
            _answerGenderMap = answerGenderMap;
            _questionGenderMap = questionGenderMap;

            _answerPronounMap = answerPronounMap;
            _questionPronounMap = questionPronounMap;
        }

        public string GetAnswerGender(GenderEnum gender)
        {
            return _answerGenderMap[gender];
        }

        public string GetAnswerPersonalPronoun(PersonalPronounEnum pronoun)
        {
            return _answerPronounMap[pronoun];
        }

        public string GetQuestionGender(GenderEnum gender)
        {
            return _questionGenderMap[gender];
        }

        public string GetQuestionPersonalPronoun(PersonalPronounEnum pronoun)
        {
            return _questionPronounMap[pronoun];
        }
    }
}
