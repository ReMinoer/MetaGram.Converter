grammar Sample_Meta;

/* Comment */

parse: EOF
{
    String s = "Hello world !";
    if (s == null)
    {
        s = "No !";
    }
}
/*
<csharp>
{
    string s = "Hello world !";
    if (string.IsNullOrEmpty(s))
    {
        s = "No !";
    }
}
*/
;