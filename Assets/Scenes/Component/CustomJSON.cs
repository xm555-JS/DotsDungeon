using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomJSON : MonoBehaviour
{
    // json파일 읽기
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = UnityEngine.JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    // json파일 변환
    public static string ToJson<T>(List<T> list)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = list;
        return JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> Items;
    }
}
