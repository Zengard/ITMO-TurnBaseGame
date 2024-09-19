using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.UI;

public class Square : MonoBehaviour
{
    public bool isActive;
    private GameObject player;

    [Space]
    public Material activeMaterial;
    public Material inactiveMaterial;

    [Space]
    public bool isRotate;
    public float duration = 0.1f;
   [SerializeField] private int health = 1;
    public bool isBroken;

    private Renderer render;

    private void Start()
    {
        render = GetComponent<Renderer>();
    }

    private void Update()
    {
        if(isRotate == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time / duration, 5) - 5);
        }
    }


    private void OnMouseUpAsButton()
    {
         player = GameObject.FindWithTag("Player");

        if(player != null)
        {
            player.GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Invoke("CallEvent", 2f);

        }       
        
    }


    public void SetSquareActive()
    {
        isActive = true;
        GetComponent<MeshRenderer>().material = activeMaterial;

    }

    public void SetSquareInactive()
    {
        var reset = GameObject.FindGameObjectsWithTag("Square");

        foreach(var square in reset)
        {
            square.GetComponent<Square>().isActive = false;
            square.GetComponent<MeshRenderer>().material = square.GetComponent<Square>().inactiveMaterial;
        }
    }

   private void CallEvent()
    {
        SetSquareInactive();
        GlobalEventManager.SendPlayerMove();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "EnemyRed" || other.gameObject.tag == "EnemyYellow")
        {
            if(isRotate == true)
            {
                if (health == 2)
                {
                    health = 1;
                    duration = 0.05f;
                    return;
                }

                if(health == 1)
                {
                    health = 0;
                    render.enabled = false;
                    isBroken = true;
                }

            }            
        }
    }

}
