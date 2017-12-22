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

        public EntryPointViewModel(IViewSwitcher viewSwitcher)
        {
            _viewSwitcher = viewSwitcher;
            _questionsButton = new ObservableButton(new Command((a) => { return true; }, (a) => StartAnsweringMode()), null);
            _learnedWordsButton = new ObservableButton(new Command((a) => { return true; }, (a) => StartLearnedWordsMode()), null);
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

        private ObservableButton _questionsButton;
        public ObservableButton QuestionsButton
        {
            get
            {
                return _questionsButton;
            }
            private set
            {
                _questionsButton = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableButton _learnedWordsButton;
        public ObservableButton LearnedWordsButton
        {
            get
            {
                return _learnedWordsButton;
            }
            private set
            {
                _learnedWordsButton = value;
                NotifyPropertyChanged();
            }
        }

        #endregion
    }
}
