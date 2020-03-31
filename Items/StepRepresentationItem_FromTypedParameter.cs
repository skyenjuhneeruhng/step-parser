using System;
using System.Collections.Generic;
using System.Diagnostics;
using StepParser.Syntax;
using StepParser.ViewModel;

namespace StepParser.Items
{
    public abstract partial class StepRepresentationItem
    {
        internal static HashSet<string> UnsupportedItemTypes { get; } = new HashSet<string>();

        /// <summary>
        /// This function is used for handle simple syntax items
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="itemSyntax"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static StepRepresentationItem FromTypedParameterToItem(StepBinder binder, StepItemSyntax itemSyntax, int id)
        {
            try
            {
                StepRepresentationItem item = null;

                var simpleItem = (StepSimpleItemSyntax)itemSyntax;
                LogWriter.Instance.WriteDebugLog(string.Format("id={0}, keyword={1}", id, simpleItem.Keyword));
                switch (simpleItem.Keyword)
                {
                    case StepItemTypeExtensions.ProductDefinitionFormationWithSpecifiedSourceText:
                        item = StepProductDefFormationWithSpecSource.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ProductText:
                        item = StepProduct.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ProductDefinitionText:
                        item = StepProductDefinition.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ShapeDefinitionRepresentationText:
                        item = StepShapeDefinitionRepresentation.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ProductDefinitionShapeText:
                        item = StepProductDefinitionShape.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ShapeRepresentationRelationshipText:
                        item = StepShapeRepresentationRelationShip.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.ShapeRepresentationText:
                        item = StepShapeRepresentation.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.AdvancedBrepShapeRepresentationText:
                        item = StepAdvancedBrepShapeRepresentation.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.StyledItemText:
                        item = StepStyledItem.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    case StepItemTypeExtensions.NextAssemblyUsageOccurrenceText:
                        item = StepNextAssemblyUsageOccrrence.CreateFromSyntaxList(binder, simpleItem.Parameters, id);
                        break;
                    default:
                        item = StepDynamicItem.CreateFromSyntaxList(binder, simpleItem, id); //Tien added on Mar 21 2020
                        break;
                }
                return item;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteDebugLog(ex);
                return null;
            }
        }

        /// <summary>
        /// This function is used for handle complex syntax item
        /// e.g: #91=(REPRESENTATION_RELATIONSHIP('','',#94,#87)REPRESENTATION_RELATIONSHIP_WITH_TRANSFORMATION(#214)SHAPE_REPRESENTATION_RELATIONSHIP());
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="itemSyntax"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal static List<StepRepresentationItem> FromTypedParameterToItems(StepBinder binder, StepItemSyntax itemSyntax, int id)
        {
            List<StepRepresentationItem> retItems = new List<StepRepresentationItem>();
            try
            {
                var complexItem = (StepComplexItemSyntax)itemSyntax;
                LogWriter.Instance.WriteDebugLog(string.Format("id={0}, syntaxType{1} start", id, complexItem.SyntaxType));
                foreach (var childItemSyntax in complexItem.Items)
                {
                    if (childItemSyntax is StepSimpleItemSyntax)
                    {
                        StepRepresentationItem item = FromTypedParameterToItem(binder, childItemSyntax, id);
                        if (item != null)
                            retItems.Add(item);
                    }
                    else
                    {
                        List<StepRepresentationItem> items = FromTypedParameterToItems(binder, childItemSyntax, id);
                        foreach (var item in items)
                            retItems.Add(item);
                    }
                }
                LogWriter.Instance.WriteDebugLog(string.Format("id={0}, syntaxType{1} end", id, complexItem.SyntaxType));
                return retItems;
            }
            catch (Exception ex)
            {
                LogWriter.Instance.WriteErrorLog(ex);
                return retItems;
            }
        }
    }
}
