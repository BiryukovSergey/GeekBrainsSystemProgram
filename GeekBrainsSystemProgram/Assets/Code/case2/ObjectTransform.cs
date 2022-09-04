using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Code.case2
{
    public struct ObjectTransform : IJobParallelFor
    {
        public NativeArray<Vector3> Positions;
        public NativeArray<Vector3> Velocities;
        public NativeArray<Vector3> FinalPositions;

        public void Execute(int index)
        {
            if (Positions.Length == Velocities.Length && Positions != null && Velocities != null)
            {
                for (int i = 0; i < FinalPositions.Length; i++)
                {
                    FinalPositions[index] = Positions[index] + Velocities[index];
                }
            }
        }
    }
}