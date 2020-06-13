using System;
using System.Collections.Generic;

public static class UniqueId
{
    private static readonly List<string> taken = new List<string>();
    private static int i = 0;

    public static string GetNewId
    {
        get
        {
            string newId = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("-", string.Empty).Replace(":", string.Empty);
            while (taken.Exists(id => id.Equals(newId)))
            {
                newId += i;
                i++;
            }
            taken.Add(newId);
            return newId;
        }
    }
}