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
            _questionsCommand = new Command((a) => { return true; }, (a) => StartAnsweringMode());
            _learnedWordsCommand = new Command((a) => { return true; }, (a) => StartLearnedWordsMode());
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

        #endregion
    }
}
