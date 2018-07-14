using UnityEngine;

/// <summary>
/// Allows a calling object to delete the object this is attached to
/// </summary>
public class DestructionCaller : MonoBehaviour
{
    public void KillThis() { DestroyImmediate(gameObject); }
}