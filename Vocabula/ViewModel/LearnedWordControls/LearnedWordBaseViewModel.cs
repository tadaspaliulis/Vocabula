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
            _addWordCommand = new Command((a) => { return true; }, (a) => AddWordInternal());
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

        private Command _addWordCommand;
        public Command AddWordCommand
        {
            get
            {
                return _addWordCommand;
            }
            private set
            {
                _addWordCommand = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
