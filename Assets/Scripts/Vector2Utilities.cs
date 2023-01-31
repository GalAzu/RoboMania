using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Utilities
{
    public static class Vector2Utilities 
    {
        public static Vector2 NewSpawnPos(float minX,float maxX, float minY, float maxY)
        {
            Vector2 newPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            return newPos;
        }
    }
}


