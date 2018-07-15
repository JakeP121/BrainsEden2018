using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TumbleweedGenerator : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject tumbleweedModel;

    /// <summary>
    /// 
    /// </summary>
    private List<GameObject> tumbleweeds = new List<GameObject>();

    void Generate()
    {
        tumbleweeds.Add(Instantiate(tumbleweedModel, new Vector3(Random.Range(-15, 15), Random.Range(1.5f, 2), Random.Range(-15, 15)), transform.rotation) as GameObject);
        tumbleweeds[tumbleweeds.Count - 1].transform.parent = transform;
    }

    void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            int rand = Random.Range(1, 10);
            if (rand == 5) Generate();
        }
    }
}