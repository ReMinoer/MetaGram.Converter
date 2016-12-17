using Antlr4.Runtime.Tree;
using MetaGram.Antlr;

namespace MetaGram
{
    public class TargetVisitor : MetaGramBaseVisitor<string>
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
            return _target == null ? base.VisitDefaultCode(context) + "\n" : "";
        }

        public override string VisitTargetCode(MetaGramParser.TargetCodeContext context)
        {
            string name = VisitTarget(context.target());
            return name == _target ? VisitCode(context.code()) + "\n" : "";
        }

        public override string VisitChildren(IRuleNode node)
        {
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
