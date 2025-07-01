using UnityEngine;

namespace KeceK.Utils
{
    public static class DebugCubeSpawner
    {

        public static void SpawnDebugCube(GameObject prefab, Vector3 position, Quaternion rotation, float deSpawnTime = 0f)
        {
            GameObject debugCube = Object.Instantiate(prefab, position, rotation);
            
            debugCube.transform.position = position;
            debugCube.transform.rotation = rotation;
            
            if(deSpawnTime > 0f)
                Object.Destroy(debugCube, deSpawnTime);
            
        }
    }
}
