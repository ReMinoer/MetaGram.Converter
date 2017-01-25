using System.Collections.Generic;
using System.Linq;
using Antlr4.Runtime.Tree;
using MetaGram.Antlr;

namespace MetaGram
{
    public class TargetVisitor : MetaGramParserBaseVisitor<string>
    {
        private readonly string _target;

        public TargetVisitor()
        {
            _target = null;
        }

        public TargetVisitor(string target)
        {
            _target = target;
        }

        public override string VisitParse(MetaGramParser.ParseContext context)
        {
            string result = base.VisitParse(context);
            return result.Substring(0, result.Length - 5);
        }

        public override string VisitDefaultCode(MetaGramParser.DefaultCodeContext context)
        {
            return _target == null || context.metaTarget().Select(VisitMetaTarget).Contains(_target)
                ? VisitDefaultCodeContent(context.defaultCodeContent())
                : "";
        }

        public override string VisitOtherCode(MetaGramParser.OtherCodeContext context)
        {
            return context.targetCode().Select(VisitTargetCode).Aggregate((x, y) => x + y);
        }

        public override string VisitTargetCode(MetaGramParser.TargetCodeContext context)
        {
            return context.metaTarget().Select(VisitMetaTarget).Contains(_target)
                ? VisitTargetCodeContent(context.targetCodeContent())
                : "";
        }

        public override string VisitChildren(IRuleNode node)
        {
            if (node == null)
                return "";

            string text = "";
            for (int i = 0; i < node.ChildCount; i++)
                text += Visit(node.GetChild(i));
            return text;
        }

        public override string VisitTerminal(ITerminalNode node)
        {
            return node.GetText();
        }
    }
}
