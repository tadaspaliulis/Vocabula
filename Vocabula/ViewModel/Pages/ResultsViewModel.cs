using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model;
using System.Collections.ObjectModel;

namespace Vocabula.ViewModel.Pages
{
    public class ResultsViewModel : BaseViewModel
    {
        private TestController _testController;
        private IViewSwitcher _viewSwitcher;

        public ResultsViewModel(TestController tester, IViewSwitcher viewSwitcher)
        {
            _testController = tester;
            _viewSwitcher = viewSwitcher;
            _questionsButton = new ObservableButton(new Command((a) => { return true; }, (a) => StartAnsweringMode()), null);
            _learnedWordsButton = new ObservableButton(new Command((a) => { return true; }, (a) => StartLearnedWordsMode()), null);
            RefreshListOfExaminationItems();
        }

        private void RefreshListOfExaminationItems()
        {
            foreach(var item in _testController.GetCurrentListOfExaminationItems())
            {
                QuestionList.Add(new ExaminationItemViewModel(item));
            }
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

        private ObservableCollection<ExaminationItemViewModel> _QuestionList = new ObservableCollection<ExaminationItemViewModel>();
        public ObservableCollection<ExaminationItemViewModel> QuestionList
        {
            get
            {
                return _QuestionList;
            }
            set
            {
                _QuestionList = value;
                NotifyPropertyChanged();
            }
        }

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
