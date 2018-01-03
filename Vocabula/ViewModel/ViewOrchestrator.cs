using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model;
using Vocabula.ViewModel.Pages;

namespace Vocabula.ViewModel
{
    public class ViewOrchestrator : ObservableObject, IViewSwitcher
    {
        public ViewOrchestrator()
        {
            SwitchView(ViewList.EntryPoint);
        }

        private bool _isAnsweringMode = false;
        public bool IsAnsweringMode
        {
            get
            {
                return _isAnsweringMode;
            }
            private set
            {
                _isAnsweringMode = value;
                NotifyPropertyChanged();
            }
        }

        private BaseViewModel _viewModel;
        public BaseViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            private set
            {
                _viewModel = value;
                NotifyPropertyChanged();
            }
        }

        public void SwitchView(ViewList view)
        {
            switch(view)
            {
                case ViewList.EntryPoint:
                    ViewModel = new EntryPointViewModel(this, _testController.GetNumberOfKnownItems());
                    break;
                case ViewList.AnsweringView:
                    ViewModel = new AnsweringViewModel(_testController, this);
                    break;
                case ViewList.ResultsView:
                    ViewModel = new ResultsViewModel(_testController, this);
                    break;
                case ViewList.LearnedView:
                    ViewModel = new LearnedItemsAddViewModel(_testController, this);
                    break;
                default:
                    throw new NotSupportedException("Unsupported View requested: " + view);
            }
        }

        private TestController _testController = new TestController();
    }
}
