using UnityEngine;

public static class ResourcesUtil
{
    public static T load<T>(this string text) where T : Object
    {
        return Resources.Load<T>(text);
    }
}
