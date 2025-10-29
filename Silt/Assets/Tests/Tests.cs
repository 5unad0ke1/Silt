using Silt;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tests : MonoBehaviour
{
    private SlotData<Slot> _data;
    void Start()
    {
        _data = SaveSystem<Common, Slot>.Slot;
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyCode.A.IsPress())
        {
            _data.Data.x++;
            Debug.Log(_data.Data.x);
        }
        if (KeyCode.Space.IsPress())
        {
            _data.Save("a");
        }
        if (KeyCode.V.IsPress())
        {
            _data.Load("a");
        }
    }
}
[System.Serializable]
public class Common : ICreatedAt, IUpdateAt, IChecksum
{
    public long createdTime;
    public long updatedTime;
    public int hash;

    long ICreatedAt.CreatedAtUnix
    {
        get => createdTime;
        set => createdTime = value;
    }
    long IUpdateAt.UpdatedAtUnix
    {
        get => updatedTime;
        set => updatedTime = value;
    }
    int IChecksum.HashNum
    {
        get => hash;
        set => hash = value;
    }
}
[System.Serializable]
public class Slot : ICreatedAt,IUpdateAt,IChecksum
{
    public long createdTime;
    public long updatedTime;
    public int hash;

    public int x; 
    public int y;

    long ICreatedAt.CreatedAtUnix
    {
        get => createdTime;
        set => createdTime = value; 
    }
    long IUpdateAt.UpdatedAtUnix 
    {
        get => updatedTime; 
        set => updatedTime = value; 
    }
    int IChecksum.HashNum 
    {
        get => hash;
        set => hash = value;
    }
}
