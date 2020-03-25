using StepParser.Syntax;
using System.Xml;

namespace StepParser.Items
{
    public class StepFaceOuterBound : StepFaceBound
    {
        public override StepItemType ItemType => StepItemType.FaceOuterBound;

        private StepFaceOuterBound()
            : base()
        {
        }

        internal static new StepFaceOuterBound CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            syntaxList.AssertListCount(3);
            var faceOuterBound = new StepFaceOuterBound();
            faceOuterBound.SyntaxList = syntaxList;
            faceOuterBound.Id = id;
            faceOuterBound.Name = syntaxList.Values[0].GetStringValue();
            faceOuterBound.BindSyntaxList(binder, syntaxList, 1);
            faceOuterBound.Orientation = syntaxList.Values[2].GetBooleanValue();
            return faceOuterBound;
        }  
    }
}
