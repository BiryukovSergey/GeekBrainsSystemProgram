using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Code.case2
{
    public class Calculation : MonoBehaviour, IDisposable
    {
        private ObjectTransform _objectTransform;

        public NativeArray<Vector3> Positions;
        public NativeArray<Vector3> Velocities;
        public NativeArray<Vector3> FinalPositions;

        private JobHandle _jobHandle;

        private void Start()
        {
            _objectTransform = new ObjectTransform()
            {
                Positions = new NativeArray<Vector3>(new Vector3[]
                {
                    new Vector3(1, 4, 1),
                    new Vector3(5, 6, 2),
                    new Vector3(4, 5, 2),
                }, Allocator.TempJob),
                Velocities = new NativeArray<Vector3>(new Vector3[]
                {
                    new Vector3(0, 2, 0),
                    new Vector3(3, 5, 7),
                    new Vector3(1, 1, 1)
                }, Allocator.TempJob),
                FinalPositions = new NativeArray<Vector3>(new Vector3[]
                {
                    new Vector3(),
                    new Vector3(),
                    new Vector3()
                }, Allocator.TempJob)
            };
            Positions = _objectTransform.Positions;
            Velocities = _objectTransform.Velocities;
            FinalPositions = _objectTransform.FinalPositions;
            SummVector3();
        }

        private void SummVector3()
        {
            _jobHandle = _objectTransform.Schedule(3, 0);
            _jobHandle.Complete();

            for (int i = 0; i < FinalPositions.Length; i++)
            {
                Debug.Log(FinalPositions[i]);
            }
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            Positions.Dispose();
            Velocities.Dispose();
            FinalPositions.Dispose();
        }
    }
}