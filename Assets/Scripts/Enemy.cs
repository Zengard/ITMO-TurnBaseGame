using UnityEngine.AI;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public GameManager gameManager;

    private void Awake()
    {
        GlobalEventManager.OnPlayerMove.AddListener(MoveToPlayer);
    }
    public void MoveToPlayer()
    {
        PathCheck();
        StartCoroutine(WaitBeforeMove());          
    }

    private void PathCheck()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Square")
            {
                hitCollider.GetComponent<Square>().isActive = true;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyRed" || collision.gameObject.tag == "EnemyYellow")
        {
            gameManager.enemies.Remove(this.gameObject);
            gameManager.enemies.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(this);
        }

        if (collision.gameObject.tag == "Player" )
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Square")
        {
            if (other.gameObject.GetComponent<Square>().isActive == true)
            {
                GlobalEventManager.SendCanMove();
                gameObject.GetComponent<NavMeshAgent>().SetDestination(other.gameObject.transform.position);
            }

            if(other.gameObject.GetComponent<Square>().isBroken == true)
            {
                gameManager.enemies.Remove(this.gameObject);     
                Destroy(this.gameObject);
            }
        }
    }


    private IEnumerator WaitBeforeMove()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(gameObject.tag == "EnemyYellow")
        {
            yield return new WaitForSeconds(2);
        }
        gameObject.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

    }
}
