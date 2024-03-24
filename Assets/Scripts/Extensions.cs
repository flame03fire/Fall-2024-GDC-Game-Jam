using System.IO;
using UnityEngine;


public class ResourcesExtension
{
    public static string ResourcesPath = Application.dataPath + "/Resources";

    public static Object Load(string resourceName, System.Type systemTypeInstance)
    {
        string[] directories = Directory.GetDirectories(ResourcesPath, "*", SearchOption.AllDirectories);
        foreach (var item in directories)
        {
            string itemPath = item.Substring(ResourcesPath.Length + 1);
            Object result = Resources.Load(itemPath + "\\" + resourceName, systemTypeInstance);
            if (result != null)
                return result;
        }
        return null;
    }
}