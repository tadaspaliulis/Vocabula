using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.ViewModel.Pages
{
    public class EntryPointViewModel : BaseViewModel
    {
        private IViewSwitcher _viewSwitcher;

        public EntryPointViewModel(IViewSwitcher viewSwitcher, int numberOfKnownWords)
        {
            _viewSwitcher = viewSwitcher;
            NumberOfKnownWords = numberOfKnownWords;

            _questionsCommand = new Command((a) => { return NumberOfKnownWords != 0; }, (a) => StartAnsweringMode());
            _learnedWordsCommand = new Command((a) => { return true; }, (a) => StartLearnedWordsMode());
        }

        private void StartAnsweringMode()
        {
            _viewSwitcher.SwitchView(ViewList.AnsweringView);
        }

        private void StartLearnedWordsMode()
        {
            _viewSwitcher.SwitchView(ViewList.LearnedView);
        }

        #region UI Properties

        private Command _questionsCommand;
        public Command QuestionsCommand
        {
            get
            {
                return _questionsCommand;
            }
            private set
            {
                _questionsCommand = value;
                NotifyPropertyChanged();
            }
        }

        private Command _learnedWordsCommand;
        public Command LearnedWordsCommand
        {
            get
            {
                return _learnedWordsCommand;
            }
            private set
            {
                _learnedWordsCommand = value;
                NotifyPropertyChanged();
            }
        }

        private int _numberOfKnownWords;
        public int NumberOfKnownWords
        {
            get
            {
                return _numberOfKnownWords;
            }
            private set
            {
                _numberOfKnownWords = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
