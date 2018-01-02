using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model
{
    /// <summary>
    /// Tracks the statistics of each individual word
    /// </summary>
    public class WordStatistics
    {
        public DateTime DateLearned;
        public DateTime LastTimeAnswered;
        public int NumberOfTimesAsked;
        public int NumberOfTimesAnsweredCorrectly;
        public string UniqueId;
    }
}
