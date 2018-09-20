using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public static GameObject ResourcePrefab;
    public GameObject Prefab;
    public void Start()
    {
        ResourcePrefab = Prefab;
    }

}
