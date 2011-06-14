// Copyright © 2011 Parttimenerd, licenced under the GPL Version 3

public static class Strings
{
    public static string TrimToLength(this string str, int length)
    {
        if (str.Length > length)
        {
             str = str.Substring(0, length - 3) + "...";
        }
        if (str.Length < length)
        {
            str.PadRight(length, ' ');
        }
        return str;
    }
    
    public static int toInteger(this string str){
    	return int.Parse(str);
    }
}