using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Abstracts;

namespace Vocabula.ViewModel.LearnedWordControls
{
    public class LearnedAdjectiveViewModel : LearnedWordBaseViewModel
    {
        private IAddLearnedWords _wordsAdder;

        public LearnedAdjectiveViewModel(IAddLearnedWords wordsAdder)
        {
            _wordsAdder = wordsAdder;
        }

        #region Base class implementations

        protected override bool AddWord()
        {
            //Try adding the word to the vocabulary
            return _wordsAdder.TryAddingLearnedAdjective(QuestionAdjective, AnswerAdjective);
        }

        protected override bool CheckIfDataValid(out string errorMessage)
        {
            if(string.IsNullOrWhiteSpace(QuestionAdjective))
            {
                errorMessage = "No valid Question Adjective";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AnswerAdjective))
            {
                errorMessage = "No valid Answer Adjective";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        protected override void Clear()
        {
            QuestionAdjective = string.Empty;
            AnswerAdjective = string.Empty;
        }

        protected override string GetDisplayableQuestionWord()
        {
            return QuestionAdjective;
        }

        protected override string GetDisplayableAnswerWord()
        {
            return AnswerAdjective;
        }

        #endregion

        #region UI Properties

        private string _questionAdjective = string.Empty;
        public string QuestionAdjective
        {
            get
            {
                return _questionAdjective;
            }
            set
            {
                _questionAdjective = value;
                NotifyPropertyChanged();
            }
        }

        private string _answerAdjective = string.Empty;
        public string AnswerAdjective
        {
            get
            {
                return _answerAdjective;
            }
            set
            {
                _answerAdjective = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
