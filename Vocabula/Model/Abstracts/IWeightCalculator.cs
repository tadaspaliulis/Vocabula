using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabula.Model.Abstracts
{
    /// <summary>
    /// An interface for calculating weight on a list of items, used by random selection
    /// </summary>
    /// <typeparam name="TObject">A type of object on which a weight can be calculated</typeparam>
    public interface IWeightCalculator<TObject>
    {
        /// <summary>
        /// A flag indicating whether the list needs to be preprocessed before calculating weights.
        /// </summary>
        bool PreprocessingNeeded { get; }

        /// <summary>
        /// Function for gathering some general stats on the entire data set,
        /// called if the the preprocessing flag is set.
        /// </summary>
        /// <param name="item"></param>
        void PreprocessItem(TObject item);

        /// <summary>
        /// A function for calculating weights for each item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A weight for the item</returns>
        int CalculateItemWeight(TObject item);

        /// <summary>
        /// Sets back any internal data back to default values.
        /// </summary>
        void Reset();
    }
}
