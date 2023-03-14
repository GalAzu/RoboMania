using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class Inetraction : MonoBehaviour
{
    private Character character;
    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
    }
    public virtual void OnInteraction()
    {
        Debug.Log("Interaction");
    }
}
