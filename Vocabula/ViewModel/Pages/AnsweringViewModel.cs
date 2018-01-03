using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model;

namespace Vocabula.ViewModel.Pages
{
    public class AnsweringViewModel : BaseViewModel
    {
        private TestController _testController;
        private IViewSwitcher _viewSwitcher;

        public AnsweringViewModel(TestController tester, IViewSwitcher viewSwitcher)
        {
            _testController = tester;
            _viewSwitcher = viewSwitcher;
            GetListOfExaminationItems();
            CheckAnswersCommand = new Command((a) => { return true; }, (a) => CheckAnswers());
        }

        private void GetListOfExaminationItems()
        {
            _QuestionList.Clear();

            var listOfQuestions = _testController.GenerateListOfExaminationItems(5);
            foreach (var item in listOfQuestions)
            {
                _QuestionList.Add(new ExaminationItemViewModel(item));
            }
        }

        private void CheckAnswers()
        {
            _testController.CheckAnswers();
            _viewSwitcher.SwitchView(ViewList.ResultsView);
        }

        #region UI Properties

        private Command _checkAnswersCommand;
        public Command CheckAnswersCommand
        {
            get
            {
                return _checkAnswersCommand;
            }
            private set
            {
                _checkAnswersCommand = value;
                NotifyPropertyChanged();
            }

        }

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

        #endregion
    }
}
