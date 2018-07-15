using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TumbleweedController : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    private Vector3 direction;

    void Start()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0) direction = Vector3.left;
        else if (rand == 1) direction = Vector3.right;
        else if (rand == 2) direction = Vector3.left + Vector3.right;
        else if (rand == 3) direction = Vector3.left - Vector3.right;
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddRelativeTorque(direction * 2.5f);
	}
}
