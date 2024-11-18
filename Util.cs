using Godot;
using Godot.Collections;

namespace CSharp;

public static class Util
{
    public static string ArrayToString<[MustBeVariant] T>(Array<T> array)
    {
        string result = "[";
        for (int i = 0; i < array.Count; i++)
        {
            result += array[i]?.ToString();
            if (i < array.Count - 1)
                result += ", "; // Add a comma and space between items
        }
        result += "]";
        return result;
    }
}