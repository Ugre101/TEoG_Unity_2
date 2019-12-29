using System;
using System.Collections.Generic;
using UnityEngine;

public static class UniqueId
{
    private static readonly string code = "1234567890";
    private static List<string> taken = new List<string>();
    private static int i = 0;

    public static string GetNewId
    {
        get
        {
            string newId = DateTime.Now.ToString().Replace(" ", string.Empty).Replace("-", string.Empty).Replace(":", string.Empty);
            while (taken.Exists(id => id.Equals(newId)))
            {
                newId += code[i];
                i = i < code.Length - 1 ? i + 1 : 0;
            }
            taken.Add(newId);
            return newId;
        }
    }
}