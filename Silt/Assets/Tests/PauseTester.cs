using Silt;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PauseTester : MonoBehaviour
{
    [SerializeField] private bool[] bools;
    [SerializeField] private AAA[] mode;

    private PauseSystem<AAA> _system; 

    public int Length => bools.Length;

    public void Add(AAA value)
    {
        _system.AddReason(value);
    }
    public void Remove(AAA value)
    {
        _system.RemoveReason(value);
    }

    void Start()
    {
        _system = new();
        for (int i = 0; i < mode.Length; i++)
        {
            mode[i] |= (AAA)(1 << Random.Range(0, 14));
        }
        for (int i = 0; i < bools.Length; i++)
        {
            Unit unit = new();
            int j = i;
            unit.OnPause += () => bools[j] = false;
            unit.OnResue += () => bools[j] = true;

            _system.Register(unit, mode[Mathf.Min(i, mode.Length - 1)]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        _system.Clear();
    }
    [Flags]
    public enum AAA : int
    {
        None = 0,
        A = 1 << 0,
        B = 1 << 1,
        C = 1 << 2,
        D = 1 << 3,
        E = 1 << 4,
        F = 1 << 5,
        G = 1 << 6,
        H = 1 << 7,
        I = 1 << 8,
        J = 1 << 9,
        K = 1 << 10,
        L = 1 << 11,
        M = 1 << 12,
        N = 1 << 13,
        O = 1 << 14,
        P = 1 << 15,
    }
    private class Unit : IPauseable
    {
        public Action OnPause;
        public Action OnResue;
        public void Pause()
        {
            OnPause?.Invoke();
        }
        public void Resume() 
        {
            OnResue?.Invoke();
        }
    }
}
