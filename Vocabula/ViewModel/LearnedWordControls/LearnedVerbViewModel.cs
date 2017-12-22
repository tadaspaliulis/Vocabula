using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Enums;
using Vocabula.Model.Abstracts;
using System.Collections.ObjectModel;

namespace Vocabula.ViewModel.LearnedWordControls
{
    public class LearnedVerbViewModel : LearnedWordBaseViewModel
    {
        private IAddLearnedWords _wordAdder;
        private IPronounContext _pronounContext;

        public LearnedVerbViewModel(IAddLearnedWords wordAdder, IPronounContext pronounContext)
        {
            _wordAdder = wordAdder;
            _pronounContext = pronounContext;

            foreach (var en in Enum.GetValues(typeof(PersonalPronounEnum)))
                _allowedPronouns.Add((PersonalPronounEnum)en);

            UpdatePersonalPronouns();
        }

        private void UpdatePersonalPronouns()
        {
            QuestionPronoun = _pronounContext.GetQuestionPersonalPronoun(SelectedPronoun);
            AnswerPronoun = _pronounContext.GetAnswerPersonalPronoun(SelectedPronoun);
        }

        #region Base class implementations

        protected override bool AddWord()
        {
            //Try adding the word to the vocabulary
            return _wordAdder.AddLearnedVerb(SelectedPronoun, QuestionVerb, AnswerVerb);
        }

        protected override bool CheckIfDataValid(out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(QuestionVerb))
            {
                errorMessage = "No valid Question Verb";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AnswerVerb))
            {
                errorMessage = "No valid Answer Verb";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        protected override void Clear()
        {
            QuestionVerb = string.Empty;
            AnswerVerb = string.Empty;
        }

        protected override string GetDisplayableQuestionWord()
        {
            return _pronounContext.GetQuestionPersonalPronoun(SelectedPronoun) + " " + QuestionVerb;
        }

        protected override string GetDisplayableAnswerWord()
        {
            return _pronounContext.GetAnswerPersonalPronoun(SelectedPronoun) + " " + AnswerVerb;
        }

        #endregion

        #region UI Properties

        private string _questionVerb = string.Empty;
        public string QuestionVerb
        {
            get
            {
                return _questionVerb;
            }
            set
            {
                _questionVerb = value;
                NotifyPropertyChanged();
            }
        }

        private string _answerVerb = string.Empty;
        public string AnswerVerb
        {
            get
            {
                return _answerVerb;
            }
            set
            {
                _answerVerb = value;
                NotifyPropertyChanged();
            }
        }

        private PersonalPronounEnum _selectedPronoun = PersonalPronounEnum.I;
        public PersonalPronounEnum SelectedPronoun
        {
            get
            {
                return _selectedPronoun;
            }
            set
            {
                _selectedPronoun = value;
                NotifyPropertyChanged();

                //Update the pronouns displayed in the UI
                UpdatePersonalPronouns();
            }
        }

        private ObservableCollection<PersonalPronounEnum> _allowedPronouns = new ObservableCollection<PersonalPronounEnum>();
        public ObservableCollection<PersonalPronounEnum> AllowedPronouns
        {
            get
            {
                return _allowedPronouns;
            }
            private set
            {
                _allowedPronouns = value;
                NotifyPropertyChanged();
            }
        }

        private string _questionPronoun;
        public string QuestionPronoun
        {
            get
            {
                return _questionPronoun;
            }
            private set
            {
                _questionPronoun = value;
                NotifyPropertyChanged();
            }
        }

        private string _answerPronoun;
        public string AnswerPronoun
        {
            get
            {
                return _answerPronoun;
            }
            private set
            {
                _answerPronoun = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
