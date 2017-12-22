using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.ViewModel
{
    public enum ViewList
    {
        EntryPoint,
        AnsweringView,
        ResultsView,
        LearnedView
    }

    public interface IViewSwitcher
    {
        void SwitchView(ViewList view);
    }
}
