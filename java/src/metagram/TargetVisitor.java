package metagram;

import metagram.antlr.*;
import org.antlr.v4.runtime.tree.*;

import java.util.List;

public class TargetVisitor extends MetaGramBaseVisitor<String>
{
    private String _target;

    public TargetVisitor()
    {
        _target = null;
    }
    public TargetVisitor(String target)
    {
        _target = target;
    }

    @Override
    public String visitParse(MetaGramParser.ParseContext ctx)
    {
        String result =  super.visitParse(ctx);
        return result.substring(0, result.length() - 5);
    }

    @Override
    public String visitDefaultCode(MetaGramParser.DefaultCodeContext ctx)
    {
        return _target == null ? super.visitDefaultCode(ctx) + "\n" : "";
    }

    @Override
    public String visitTargetCode(MetaGramParser.TargetCodeContext ctx)
    {
        String name = visitTarget(ctx.target());
        return name.equals(_target) ? visitCode(ctx.code()) + "\n" : "";
    }

    @Override
    public String visitChildren(RuleNode node)
    {
        String text = "";
        for (int i = 0; i < node.getChildCount(); i++)
            text += visit(node.getChild(i));
        return text;
    }

    @Override
    public String visitTerminal(TerminalNode node)
    {
        return node.getText();
    }

    @Override
    protected String aggregateResult(String aggregate, String nextResult)
    {
        return aggregate + nextResult;
    }
}
