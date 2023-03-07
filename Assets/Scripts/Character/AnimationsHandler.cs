using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationsHandling
{
    public  class AnimationsHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        public void InstantiateVFX(GameObject vfx, Transform _transform)
        {
            Instantiate(vfx, _transform.position , Quaternion.identity);
        }
    }
}
