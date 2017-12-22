using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model;
using Vocabula.Model.Enums;
using Vocabula.ViewModel.LearnedWordControls;

namespace Vocabula.ViewModel.Pages
{
    public class LearnedItemsAddViewModel : BaseViewModel
    {
        private IViewSwitcher _viewSwitcher;
        private TestController _testController;

        public LearnedItemsAddViewModel(TestController testController, IViewSwitcher viewSwitcher)
        {
            _viewSwitcher = viewSwitcher;
            _questionsButton = new ObservableButton(new Command((a) => { return true; }, (a) => StartAnsweringMode()), null);
            _testController = testController;

            foreach (var en in Enum.GetValues(typeof(LearnedWordTypeEnum)))
                _allowedWordTypes.Add((LearnedWordTypeEnum)en);

            _messageDisplay = "Add the new words that you've learned!";

            UpdateLearnedWordControl();
        }

        private void StartAnsweringMode()
        {
            _viewSwitcher.SwitchView(ViewList.AnsweringView);
        }

        private void LearnedControlMessageHandler(object sender, string message)
        {
            MessageDisplay = message;
        }

        /// <summary>
        /// Change the displayed word adding control to match the currently selected type
        /// </summary>
        private void UpdateLearnedWordControl()
        {
            switch (LearnedWordTypeSelection)
            {
                case LearnedWordTypeEnum.Adjective:
                    LearnedItemControl = new LearnedAdjectiveViewModel(_testController);
                    break;
                case LearnedWordTypeEnum.Noun:
                    LearnedItemControl = new LearnedNounViewModel(_testController, _testController.GetLanguageContext());
                    break;
                case LearnedWordTypeEnum.Verb:
                    LearnedItemControl = new LearnedVerbViewModel(_testController, _testController.GetLanguageContext());
                    break;
                default:
                    throw new NotSupportedException(LearnedWordTypeSelection + " is not a supported type");
            }

            LearnedItemControl.WordAddEvent += LearnedControlMessageHandler;
        }

        #region UI Properties

        private LearnedWordTypeEnum _learnedWordTypeSelection = LearnedWordTypeEnum.Adjective;
        public LearnedWordTypeEnum LearnedWordTypeSelection
        {
            get
            {
                return _learnedWordTypeSelection;
            }
            set
            {
                if (_learnedWordTypeSelection != value)
                {
                    _learnedWordTypeSelection = value;
                    NotifyPropertyChanged();
                    UpdateLearnedWordControl();
                }
            }
        }

        private ObservableCollection<LearnedWordTypeEnum> _allowedWordTypes = new ObservableCollection<LearnedWordTypeEnum>();
        public ObservableCollection<LearnedWordTypeEnum> AllowedWordTypes
        {
            get
            {
                return _allowedWordTypes;
            }
            private set
            {
                _allowedWordTypes = value;
                NotifyPropertyChanged();
            }
        }

        private string _messageDisplay;
        public string MessageDisplay
        {
            get
            {
                return _messageDisplay;
            }
            set
            {
                _messageDisplay = value;
                NotifyPropertyChanged();
            }
        }

        private LearnedWordBaseViewModel _learnedItemControl;
        public LearnedWordBaseViewModel LearnedItemControl
        {
            get
            {
                return _learnedItemControl;
            }
            set
            {
                _learnedItemControl = value;
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

        #endregion
    }
}
