using System.Collections;
using System.Collections.Generic;
using System;

public class Utils
{
    private static string GetLetters(string str)
    {


        char[] arr = str.ToCharArray();

        arr = Array.FindAll<char>(arr, (c => (char.IsLetter(c)
                                          //|| char.IsWhiteSpace(c)
                                          //|| c == '-'
                                          )));
        str = new string(arr);

        return str;
    }
}
