using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class JsonClass
{
    public int i;
    public float f;
    public bool b;
    public string str;
    public int[] iArray;
    public List<int> iList = new List<int>();
    public Dictionary<string, float> fDict = new Dictionary<string, float>();

    public JsonClass() { }

    public JsonClass(bool isSet)
    {
        i = 1;
        f = 2.0f;
        b = true;
        str = "Hello";
        iArray = new int[] { 1, 2, 3, 4, 5 };
        iList.Add(1);
        iList.Add(2);
        iList.Add(3);
        fDict.Add("First", 1.0f);
        fDict.Add("Second", 2.0f);
        fDict.Add("Third", 3.0f);
    }

    public void Print()
    {
        Debug.Log("i: " + i);
        Debug.Log("f: " + f);
        Debug.Log("b: " + b);
        Debug.Log("str: " + str);
        foreach (int i in iArray)
        {
            Debug.Log("iArray: " + i);
        }
        foreach (int i in iList)
        {
            Debug.Log("iList: " + i);
        }
        foreach (KeyValuePair<string, float> pair in fDict)
        {
            Debug.Log("fDict: " + pair.Key + ", " + pair.Value);
        }

    }
}

public class JsonTest : MonoBehaviour
{
    private void Start()
    {
        var jtc2 = LoadJsonFile<JsonClass>(Application.dataPath, "JsonTest");
        jtc2.Print();

        // JsonClass jsonClass = new JsonClass(true);
        // string jsonData = ObjectToJson(jsonClass);
        // CreateJsonFile(Application.dataPath, "JsonTest", jsonData);
        // Debug.Log(jsonData);

        // var newJsonClass = JsonToOject<JsonClass>(jsonData);
        // newJsonClass.Print();
    }

    private string ObjectToJson(object obj) { return JsonConvert.SerializeObject(obj); }
    private T JsonToOject<T>(string jsonData) { return JsonConvert.DeserializeObject<T>(jsonData); }

    private void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
    }

    private T LoadJsonFile<T>(string loadPath, string fileName)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonToOject<T>(jsonData);
    }

}
