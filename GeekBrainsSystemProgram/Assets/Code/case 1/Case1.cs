using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Case1 : MonoBehaviour, IDisposable
{
    private NativeArray<int> _intArray;
    private Work _work;
    
    private JobHandle _jobHandle;

    private void Start()
    {
        _intArray = new NativeArray<int>(new int[] {1, 43, 56, 22, 10, 9, 5}, Allocator.TempJob);
        _work = new Work()
        {
            _workArray = _intArray
        };
        
        _jobHandle = _work.Schedule();
        _jobHandle.Complete();
        
        for (int i = 0; i < _intArray.Length; i++)
        {
            Debug.Log(_intArray[i]);
        }
    }

    public void Dispose()
    {
        _intArray.Dispose();
    }

    public void OnDestroy()
    {
        Dispose();
    }
}

public struct Work : IJob
{
    public NativeArray<int> _workArray;

    public void Execute()
    {
        for (int i = 0; i < _workArray.Length; i++)
        {
            if (_workArray[i] > 10)
            {
                _workArray[i] = 0;
            }
        }
    }
}