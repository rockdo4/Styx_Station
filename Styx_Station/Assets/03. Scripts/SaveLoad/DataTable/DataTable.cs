using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataTable<T> where T : DataTable<T>, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
    protected DataTable() { }
    protected string path { get; set; }
    public abstract void Load();
}