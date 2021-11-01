using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class EnvLoader
{
    public static void Load()
    {
        string filePath = Environment.CurrentDirectory + "\\.env";
        if (!File.Exists(filePath)) return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=', '\n');
            if (parts.Length != 2) continue;

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
            // Debug.Log("[ENVLOADER]: " + parts[0] + " = " +  parts[1]);
        }
    }
}
