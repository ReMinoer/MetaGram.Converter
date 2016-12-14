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
    public String visitDefault_language(MetaGramParser.Default_languageContext ctx)
    {
        return _target == null ? super.visitDefault_language(ctx) + "\n" : "";
    }

    @Override
    public String visitLanguage(MetaGramParser.LanguageContext ctx)
    {
        String name = visitLanguage_name(ctx.language_name());
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
