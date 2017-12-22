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
    public class LearnedNounViewModel : LearnedWordBaseViewModel
    {
        private IAddLearnedWords _wordAdder;
        private IGenderContext _genderContext;

        public LearnedNounViewModel(IAddLearnedWords wordAdder, IGenderContext genderContext)
        {
            _wordAdder = wordAdder;
            _genderContext = genderContext;

            foreach (var en in Enum.GetValues(typeof(GenderEnum)))
                _allowedGenders.Add((GenderEnum)en);

            UpdateGenderArticles();
        }

        private void UpdateGenderArticles()
        {
            QuestionGenderArticle = _genderContext.GetQuestionGender(SelectedGender);
            AnswerGenderArticle = _genderContext.GetAnswerGender(SelectedGender);
        }

        #region Base class implementations

        protected override bool AddWord()
        {
            //Try adding the word to the vocabulary
            return _wordAdder.AddLearnedNoun(SelectedGender, QuestionNoun, AnswerNoun);
        }

        protected override bool CheckIfDataValid(out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(QuestionNoun))
            {
                errorMessage = "No valid Question Noun";
                return false;
            }

            if (string.IsNullOrWhiteSpace(AnswerNoun))
            {
                errorMessage = "No valid Answer Noun";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        protected override void Clear()
        {
            QuestionNoun = string.Empty;
            AnswerNoun = string.Empty;
        }

        protected override string GetDisplayableQuestionWord()
        {
            return _genderContext.GetQuestionGender(SelectedGender) + " " + QuestionNoun;
        }

        protected override string GetDisplayableAnswerWord()
        {
            return _genderContext.GetAnswerGender(SelectedGender) + " " + AnswerNoun;
        }

        #endregion

        #region UI Properties

        private string _questionNoun = string.Empty;
        public string QuestionNoun
        {
            get
            {
                return _questionNoun;
            }
            set
            {
                _questionNoun = value;
                NotifyPropertyChanged();
            }
        }

        private string _answerNoun = string.Empty;
        public string AnswerNoun
        {
            get
            {
                return _answerNoun;
            }
            set
            {
                _answerNoun = value;
                NotifyPropertyChanged();
            }
        }

        private GenderEnum _selectedGender = GenderEnum.Neutral;
        public GenderEnum SelectedGender
        {
            get
            {
                return _selectedGender;
            }
            set
            {
                _selectedGender = value;
                NotifyPropertyChanged();

                //Update the articles displayed in the UI
                UpdateGenderArticles();
            }
        }

        private ObservableCollection<GenderEnum> _allowedGenders = new ObservableCollection<GenderEnum>(); 
        public ObservableCollection<GenderEnum> AllowedGenders
        {
            get
            {
                return _allowedGenders;
            }
            private set
            {
                _allowedGenders = value;
                NotifyPropertyChanged();
            }
        }

        private string _questionGenderArticle;
        public string QuestionGenderArticle
        {
            get
            {
                return _questionGenderArticle;
            }
            private set
            {
                _questionGenderArticle = value;
                NotifyPropertyChanged();
            }
        }

        private string _answerGenderArticle;
        public string AnswerGenderArticle
        {
            get
            {
                return _answerGenderArticle;
            }
            private set
            {
                _answerGenderArticle = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
