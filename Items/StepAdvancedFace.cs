using System.Linq;
using System.Xml;
using StepParser.Syntax;

namespace StepParser.Items
{
    public class StepAdvancedFace : StepFaceSurface
    {
        public override StepItemType ItemType => StepItemType.AdvancedFace;
        public StepAdvancedFace(string name)
            : base(name)
        {
        }

        private StepAdvancedFace()
            : base(string.Empty)
        {
        }

        internal static new StepAdvancedFace CreateFromSyntaxList(StepBinder binder, StepSyntaxList syntaxList, int id)
        {
            var face = new StepAdvancedFace();
            face.SyntaxList = syntaxList;
            syntaxList.AssertListCount(4);
            face.Id = id;
            face.Name = syntaxList.Values[0].GetStringValue();
            face.BindSyntaxList(binder, syntaxList, 1, 3);
            face.SameSense = syntaxList.Values[3].GetBooleanValue();

            return face;
        }
    }
}
