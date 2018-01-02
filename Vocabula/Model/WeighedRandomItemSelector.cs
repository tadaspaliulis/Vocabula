using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabula.Model.Abstracts;

namespace Vocabula.Model
{
    /// <summary>
    /// Randomly picks a subset of items from a list,
    /// while giving some of them a higher chance of selection based on a weight.
    /// </summary>
    public class WeighedRandomItemSelector<TObject>
    {
        public WeighedRandomItemSelector(List<TObject> items, Random randomizer, IWeightCalculator<TObject> weightCalculator)
        {
            _randomizer = randomizer;
            _items = items;
            _weightCalculator = weightCalculator;
        }
       
        public List<TObject> GetRandomItems(int requestedCount)
        {
            //Check if the requested count is higher than the number available items
            if (_items.Count < requestedCount)
                requestedCount = _items.Count;

            var selectedItems = new List<TObject>();

            //Prepare a linked list with weights
            var weighedList = PrepareWeighedLinkedList();
            var totalWeightLeft = _totalWeight;
            
            //Keep going till we have the requested number of items
            while(selectedItems.Count < requestedCount)
            {
                var randomNumber = _randomizer.Next(0, totalWeightLeft);
                var weightSumSoFar = 0;

                //Iterate through the linked list
                var node = weighedList.First;
                while (node != null)
                {
                    var next = node.Next;

                    if (randomNumber < (weightSumSoFar + node.Value.Item1))
                    {
                        //Remove the weight from the total weight left
                        totalWeightLeft -= node.Value.Item1;

                        //We've selected an item, add it to the return list
                        selectedItems.Add(node.Value.Item2);

                        //And now remove the node itself and break out of the loop
                        weighedList.Remove(node);
                        break;
                    }

                    weightSumSoFar += node.Value.Item1;
                    node = next;
                }
            }

            return selectedItems;
        }

        private LinkedList<Tuple<int, TObject>> PrepareWeighedLinkedList()
        {
            _totalWeight = 0;
            var linkedList = new LinkedList<Tuple<int, TObject>>();
            
            //Preprocess the items first
            if (_weightCalculator.PreprocessingNeeded)
            {
                _weightCalculator.Reset();

                foreach (var item in _items)
                    _weightCalculator.PreprocessItem(item);
            }

            //then calculate weights and it to the linked list
            foreach(var item in _items)
            {
                var weight = _weightCalculator.CalculateItemWeight(item);
                _totalWeight += weight;
                linkedList.AddLast(new LinkedListNode<Tuple<int, TObject>>(new Tuple<int, TObject>(weight, item)));
            }

            return linkedList;
        }

        private IWeightCalculator<TObject> _weightCalculator;
        private Random _randomizer;
        private int _totalWeight;

        private List<TObject> _items;
    }
}
