using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model
{
    public class ExaminationItem
    {
        public ExaminationItem(MemorisableItem memorisableItem)
        {
            _memorisableItem = memorisableItem;
            SubmittedAnswer = string.Empty;
            IsAnswerCorrect = false;
        }

        public void CheckAnswer()
        {
            IsAnswerCorrect = string.Compare(_memorisableItem.GetTheAnswer(), SubmittedAnswer, true) == 0;
        }

        public string Question
        {
            get
            {
                return _memorisableItem.GetTheQuestion();
            }
        }

        public string CorrectAnswer
        {
            get
            {
                return _memorisableItem.GetTheAnswer();
            }
        }

        public string SubmittedAnswer { get; set; }
        public bool IsAnswerCorrect { get; private set; }

        private MemorisableItem _memorisableItem;
    }
}
