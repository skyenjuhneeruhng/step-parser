using StepParser.Items;
using StepParser.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StepParser
{
    internal class StepBinder
    {
        private Dictionary<int, List<StepRepresentationItem>> _itemMap;
        private Dictionary<int, List<Tuple<StepSyntax, Action<StepBoundItem>>>> _unboundPointers = new Dictionary<int, List<Tuple<StepSyntax, Action<StepBoundItem>>>>();

        public StepBinder(Dictionary<int, List<StepRepresentationItem>> itemMap)
        {
            _itemMap = itemMap;
        }

        public void BindValue(StepSyntax syntax, Action<StepBoundItem> bindAction)
        {
            if (syntax is StepSimpleItemSyntax)
            {
                var typedParameter = (StepSimpleItemSyntax)syntax;
                var item = StepRepresentationItem.FromTypedParameterToItem(this, typedParameter, typedParameter.Id);
                var boundItem = new StepBoundItem(item, syntax);
                bindAction(boundItem);
            }
            else if (syntax is StepEntityInstanceReferenceSyntax)
            {
                var itemInstance = (StepEntityInstanceReferenceSyntax)syntax;
                if (_itemMap.ContainsKey(itemInstance.Id))
                {
                    // pointer already defined, bind immediately
                    foreach (var item in _itemMap[itemInstance.Id])
                    {
                        var boundItem = new StepBoundItem(item, syntax);
                        bindAction(boundItem);
                    }
                }
                else
                {
                    // not already defined, save it for later
                    if (!_unboundPointers.ContainsKey(itemInstance.Id))
                    {
                        _unboundPointers.Add(itemInstance.Id, new List<Tuple<StepSyntax, Action<StepBoundItem>>>());
                    }

                    _unboundPointers[itemInstance.Id].Add(Tuple.Create(syntax, bindAction));
                }
            }
            else if (syntax is StepAutoSyntax)
            {
                bindAction(StepBoundItem.AutoItem(syntax));
            }
            else
            {
                throw new StepReadException("Unable to bind pointer, this should be unreachable", syntax.Line, syntax.Column);
            }
        }

        public void BindRemainingValues()
        {
            foreach (var id in _unboundPointers.Keys)
            {
                if (!_itemMap.ContainsKey(id))
                {
                    var syntax = _unboundPointers[id].First().Item1;
                    continue;
                    //throw new StepReadException($"Cannot bind undefined pointer {id}", syntax.Line, syntax.Column);
                }

                var items = _itemMap[id];
                foreach(var item in items)
                {
                    foreach (var binder in _unboundPointers[id])
                    {
                        var boundItem = new StepBoundItem(item, binder.Item1);
                        binder.Item2(boundItem);
                    }
                }
            }
        }
    }
}
