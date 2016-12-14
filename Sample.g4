grammar Sample;

/* Comment */

@header { package org.test; }
@members { private int number = 0; }
/* <csharp> @members { private int number = 0; } */

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