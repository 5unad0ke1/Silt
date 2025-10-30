using Core;
using UnityEngine;

/// <summary>
/// MonoPoolManagerを指定できるように実装する
/// </summary>
/// <typeparam name="TType">オブジェクトプールする型</typeparam>
public interface IMonoPool<TType> where TType : Behaviour
{
    public void SetMonoPoolManager(MonoObjectPool<TType> manager);
}