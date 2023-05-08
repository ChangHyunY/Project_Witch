using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    T Get(string name);
    void ReturnObject(T obj);
}
