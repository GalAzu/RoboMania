using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace _Utilities
{
    public static class Vector2Utilities 
    {
        public static Vector2 NewSpawnPos()
        {
            Vector2 newPos = new Vector2(Random.Range(-30,30), Random.Range(-30, 30));
            return newPos;
        }
    }
}


