using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Storage.DataFormats;
using Vocabula.Model.Storage.DataFormats.Version1;

namespace Vocabula.Model.Storage
{
    /// <summary>
    /// Responsible for serialising and deserializing the statistics of each word
    /// </summary>
    public class StatisticsSerialiser : BaseDataSerialiser<WordStatsListV1>
    {
        private static DataFileVersionNumber CurrentFileVersion = new DataFileVersionNumber(1, 0, 0);

        public StatisticsSerialiser(string dataStoraPath) : base(dataStoraPath)
        {
        }

        public void AddStatisticsItem(Statistics item)
        {
            _fileData.WordStats.Add(ConvertToDataRecord(item));
        }

        public List<Statistics> GenerateListOfStatistics()
        {
            var statsList = new List<Statistics>();
            foreach (var item in _fileData.WordStats)
                statsList.Add(ConvertFromDataRecord(item));

            return statsList;
        }

        protected override WordStatsListV1 NewFileDataObject()
        {
            return new WordStatsListV1
            {
                VersionNumber = CurrentFileVersion,
                WordStats = new List<WordStatV1>()
            };
        }

        private WordStatV1 ConvertToDataRecord(Statistics item)
        {
            return new WordStatV1
            {
                DateLearned = item.DateLearned,
                LastTimeAnswered = item.LastTimeAnswered,
                NumberOfTimesAnsweredCorrectly = item.NumberOfTimesAnsweredCorrectly,
                NumberOfTimesAsked = item.NumberOfTimesAsked,
                UniqueId = item.UniqueId,
            };
        }

        private Statistics ConvertFromDataRecord(WordStatV1 item)
        {
            return new Statistics
            {
                DateLearned = item.DateLearned,
                LastTimeAnswered = item.LastTimeAnswered,
                NumberOfTimesAnsweredCorrectly = item.NumberOfTimesAnsweredCorrectly,
                NumberOfTimesAsked = item.NumberOfTimesAsked,
                UniqueId = item.UniqueId,
            };
        }
    }
}
