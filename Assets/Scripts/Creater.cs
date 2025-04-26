using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    public List<GameObject> RandomList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject RandSearch(GameObject before = null)
    {
        int a = 0;
        List<GameObject> list = new List<GameObject>(RandomList);
        int index = UnityEngine.Random.Range(0, list.Count);
        list.Remove(before);
        foreach (GameObject go in list)
        {
            if (index == a)
            {
                return go;
            }
            else
            {
                a++;
            }
        }
        return null;
    }
    public GameObject RandSearch()
    {
        int index = UnityEngine.Random.Range(0, RandomList.Count);
        int a = 0;
        foreach (GameObject go in RandomList)
        {
            if (index == a)
            {
                return go;
            }
            else
            {
                a++;
            }
        }
        return null;
    }

    public GameObject CreatEvent(GameObject _Event)
    {
        return Instantiate(_Event);
    }
    public GameObject CreatEvent(GameObject _Event,Vector3 _transform)
    {
        GameObject obj;
        obj = Instantiate(_Event);
        obj.transform.position = _transform;
        return obj;
    }
}
