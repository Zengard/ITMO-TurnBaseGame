using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        GlobalEventManager.OnPlayerCanMove.AddListener(PathCheck);
    }

    private void Start()
    {
        PathCheck();
    }


    private void PathCheck()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        foreach(var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "Square")
            {
                hitCollider.GetComponent<Square>().SetSquareActive();
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyRed" || collision.gameObject.tag == "EnemyYellow")
        {     
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Square")
        {
            if (other.gameObject.GetComponent<Square>().isBroken == true)
            {  
                Destroy(this.gameObject);
            }
        }
    }

}
