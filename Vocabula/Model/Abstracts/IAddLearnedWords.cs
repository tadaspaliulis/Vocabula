using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;

namespace Vocabula.Model.Abstracts
{
    public interface IAddLearnedWords
    {
        bool TryAddingLearnedAdjective(string questionWord, string answerWord);
        bool AddLearnedVerb(PersonalPronounEnum personalPronoun, string questionWord, string answerWord);
        bool AddLearnedNoun(GenderEnum gender, string questionWord, string answerWord);
    }
}
