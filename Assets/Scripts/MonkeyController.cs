using UnityEngine;

/// <summary>
/// Allows other scripts to control how the monkey is being animated 
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
    /// 
    /// </summary>
    [SerializeField]
    private GameObject[] monkeyDances;

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
        //initialise new model representing the change and removed old character
        GameObject newModel = Instantiate(monkeyNormalStance, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;

        //play animation
        anim.Play("Idle");
        if (debug) Debug.Log("Player " + transform.name + " is standing");
    }

    /// <summary>
    /// Makes the monkey shoot to the left (of the screen)
    /// </summary>
    public void ShootLeft()
    {
        //initialise new model representing the change and removed old character
        GameObject newModel = Instantiate(monkeyShootingStance, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;

        //play animation
        anim.Play("Shoot");
        if (debug) Debug.Log("Player " + transform.name + " is shooting left");
    }

    /// <summary>
    /// Makes the monkey shoot to the right (of the screen)
    /// </summary>
    public void ShootRight()
    {
        //initialise new model representing the change and removed old character
        GameObject newModel = Instantiate(monkeyShootingStance, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        newModel.transform.Rotate(new Vector3(0, -180, 0));
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;

        //play animation
        anim.Play("Shoot");
        if (debug) Debug.Log("Player " + transform.name + " is shooting right");
    }

    /// <summary>
    /// Makes the monkey dance (useful for victory dances)
    /// </summary>
    public void Dance()
    {
        //escape clause for if no dances are specified
        if (monkeyDances.Length == 0) return;

        //randomly select dance
        int selectedDance = Random.Range(0, monkeyDances.Length - 1);

        //initialise new model representing the change and removed old character
        GameObject newModel = Instantiate(monkeyDances[selectedDance], transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        newModel.transform.parent = transform;
        newModel.transform.localScale = new Vector3(1, 1, 1);
        newModel.transform.Rotate(new Vector3(0, -180, 0));
        anim = newModel.GetComponent<Animator>();
        transform.GetChild(0).GetComponent<DestructionCaller>().KillThis();

        //set animation speed
        anim.speed = speedOfAnimation;

        //play animation
        anim.Play(newModel.transform.name.Substring(0, newModel.transform.name.Length - 7));
        if (debug) Debug.Log("Player " + transform.name + " is dancing");
    }
}
