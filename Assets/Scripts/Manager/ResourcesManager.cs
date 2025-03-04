using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

public class ResourcesManager : SingletonManager<ResourcesManager>
{
    //Reference Check
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    //LevelCs
    private Dictionary<int, LevelData> resourcesScripts = new Dictionary<int, LevelData>();

    public LevelData LoadScript(int num)
    {
        if (!resourcesScripts.ContainsKey(num))
        {
            resourcesScripts.Add(num, Resources.Load<LevelData>("Levels/LevelData_" + num));
        }

        return resourcesScripts.GetValueOrDefault(num);
    }

    //CSV File Read
    string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    char[] TRIM_CHARS = { '\"' };

    public List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load("TextCSV/" + file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                value = value.Replace("</n>", "\n");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
