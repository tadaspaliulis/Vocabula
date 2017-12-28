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

            //Update the stats for this word
            UpdateStatistics();
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

        private void UpdateStatistics()
        {
            var stats = _memorisableItem.statistics;

            stats.LastTimeAnswered = DateTime.Now;
            stats.NumberOfTimesAsked += 1;
            if (IsAnswerCorrect)
                stats.NumberOfTimesAnsweredCorrectly += 1;
        }

        private MemorisableItem _memorisableItem;
    }
}
