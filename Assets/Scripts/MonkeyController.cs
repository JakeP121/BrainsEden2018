using UnityEngine;

/// <summary>
/// 
/// </summary>
public class MonkeyController : MonoBehaviour
{
    /// <summary>
    /// Flags whether the code should print out debug logs
    /// </summary>
    [SerializeField]
    private bool debug = true;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject monkeyNormalStance;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private GameObject monkeyShootingStance;

    /// <summary>
    /// Controls the speed of the animations
    /// </summary>
    [SerializeField]
    private int speedOfAnimation = 1;

    /// <summary>
    /// Local reference to the monkey's Animator component
    /// </summary>
    private Animator anim;

    void Start()
    {
        //go to default state
        NormalStance();
    }

    /// <summary>
    /// Makes the monkey jump to a normal stance
    /// </summary>
    public void NormalStance()
    {
        GameObject newModel = Instantiate(monkeyNormalStance, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        newModel.transform.position = new Vector3(0f, 1f, 0f);
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;
        anim.Play("Idle");
        if (debug) Debug.Log("Player " + transform.name + " returning to normal stance");
    }

    /// <summary>
    /// Makes the monkey shoot to the left (of the screen)
    /// </summary>
    public void ShootLeft()
    {
        GameObject newModel = Instantiate(monkeyShootingStance, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        newModel.transform.position = new Vector3(0f, 1f, 0f);
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;
        anim.Play("Shoot");
        if (debug) Debug.Log("Player " + transform.name + " shooting left");
    }

    /// <summary>
    /// Makes the monkey shoot to the right (of the screen)
    /// </summary>
    public void ShootRight()
    {
        GameObject newModel = Instantiate(monkeyShootingStance, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        newModel.transform.position = new Vector3(0f, 1f, 0f);
        newModel.transform.Rotate(new Vector3(0, -180, 0));
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;
        anim.Play("Shoot");
        if (debug) Debug.Log("Player " + transform.name + " shooting right");
    }
}
