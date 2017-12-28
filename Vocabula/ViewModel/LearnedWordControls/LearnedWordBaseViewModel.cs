using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.ViewModel.LearnedWordControls
{
    public abstract class LearnedWordBaseViewModel : ObservableObject
    {
        protected LearnedWordBaseViewModel()
        {
            _addWordButton = new ObservableButton(new Command((a) => { return true; }, (a) => AddWordInternal()), null);
        }

        private void AddWordInternal()
        {
            if (!CheckIfDataValid(out string errorMessage))
            {
                //Input validation failed
                InvokeWordAddEvent(errorMessage);
                return;
            }

            //Try adding the new word
            if (!AddWord()) 
            {
                //If false, the word already exists in the dictionary!
                InvokeWordAddEvent(GetDisplayableQuestionWord() + " has already been learned.");
                return;
            }

            //Display the added word
            InvokeWordAddEvent("Added " + GetDisplayableAnswerWord());

            Clear();
        }

        private void InvokeWordAddEvent(string message)
        {
            WordAddEvent?.Invoke(this, message);
        }

        /// <summary>
        /// Invoked whenever a new word is added so the container object could display it
        /// </summary>
        public EventHandler<string> WordAddEvent;

        #region Abstract methods and functions

        protected abstract string GetDisplayableQuestionWord();

        protected abstract string GetDisplayableAnswerWord();

        protected abstract bool AddWord();

        protected abstract bool CheckIfDataValid(out string errorMessage);

        protected abstract void Clear();

        #endregion

        #region UI Properties

        private ObservableButton _addWordButton;
        public ObservableButton AddWordButton
        {
            get
            {
                return _addWordButton;
            }
            private set
            {
                _addWordButton = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
