using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Inetraction : MonoBehaviour
{
    public delegate void OnInteraction();
    public static event OnInteraction OnInteractionEvent;
}
