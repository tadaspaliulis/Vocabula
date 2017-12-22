using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model;

namespace Vocabula.ViewModel.Pages
{
    public class ExaminationItemViewModel : ObservableObject
    {
        public ExaminationItemViewModel(ExaminationItem item)
        {
            _item = item;
        }

        public string Question
        {
            get
            {
                return _item.Question;
            }
        }

        public string CorrectAnswer
        {
            get
            {
                return _item.CorrectAnswer;
            }
        }

        public string SubmittedAnswer
        {
            get
            {
                return _item.SubmittedAnswer;
            }
            set
            {
                _item.SubmittedAnswer = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsAnswerCorrect
        {
            get
            {
                return _item.IsAnswerCorrect;
            }
        }

        private ExaminationItem _item;
    }
}
