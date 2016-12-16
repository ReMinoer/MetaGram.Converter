grammar MetaGram;

parse: (default_language | language | QUOTE | TOKENS | OTHERS)* EOF;
default_language: DEFAULT_LANGUAGE;
language: language_name code;
language_name: LANGUAGE_NAME;
code: CODE;

LANGUAGE_NAME: '/*' VOID? '<' VOID? [a-zA-Z0-9]+ VOID? '>'
    {
        String s = getText().trim();
        s = s.substring(2, s.length() - 1).trim();
        s = s.substring(1, s.length()).trim();
        setText(s);
    }
    /*
    <csharp>
    {
        string s = Text.Trim();
        s = s.Substring(2, s.Length - 3).Trim();
        s = s.Substring(1, s.Length - 1).Trim();
        Text = s;
    }
    */
    ;

CODE: VOID? ('@header' | '@members')? VOID? '{' (INNER_CODE | ~[}])* '}' VOID? '*/' VOID?
    {
        String s = getText().trim();
        s = s.substring(0, s.length() - 2).trim();
        setText(s);
    }
    /*
    <csharp>
    {
        string s = Text.Trim();
        s = s.Substring(0, s.Length - 2).Trim();
        Text = s;
    }
    */
    ;

DEFAULT_LANGUAGE: ('@header' | '@members')? VOID? '{' (INNER_CODE | ~[}])* '}' VOID?
    {
        setText(getText().trim());
    }
    /*
    <csharp>
    {
        Text = Text.Trim();
    }
    */
    ;

QUOTE: '\'' (~['] | '\\\'')* '\'';

TOKENS: ['/*<>{}@]+;
OTHERS: ~['/*<>{}@]+;

fragment INNER_CODE: VOID '{' (INNER_CODE | ~[}])* '}' VOID;
fragment VOID: (' ' | '\t' | '\n' | '\r')+;