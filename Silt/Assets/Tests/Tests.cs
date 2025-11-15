using Silt;
using Silt.Runtime;
using UnityEngine;

public sealed class Tests : MonoBehaviour
{
    private SlotData<Slot> _data;
    void Start()
    {
        _data = SaveSystem<Common, Slot>.Slot;
    }
    public void Load()
    {
        _data.Load("a");
    }
    public void Save()
    {
        _data.Save("a");
    }
    public void Incre()
    {
        _data.Data.x++;
        Debug.Log(_data.Data.x);
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
public class Slot : ICreatedAt, IUpdateAt, IChecksum
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
