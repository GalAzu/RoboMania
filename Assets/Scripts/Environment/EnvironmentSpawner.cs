using System.Collections.Generic;
using UnityEngine;
using _Utilities;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField]
    private List<ExplosiveItems> explodingObjects = new(); //Refactor ResponsiveObjects to scriptable objects
    private CameraFollow camera;
    public int numberToSpawn;

    private void Awake()
    {
        camera = Camera.main.GetComponent<CameraFollow>();
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        var randomNumber = Random.Range(0, explodingObjects.Count);
        for(int i = 0; i < numberToSpawn;i++ )
        {

            Instantiate(explodingObjects[randomNumber],Vector2Utilities.NewSpawnPos(camera.x_minVal , camera.x_maxVal,camera.y_minVal,camera.y_maxVal),Quaternion.identity);
        }
    }
}
