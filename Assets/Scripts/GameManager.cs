using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();

    [Space]
    public GameObject gameOver;
    public GameObject nextLevel;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies.Count == 0)
        {
            Debug.Log("Next level");
            nextLevel.SetActive(true);
        }

        if (player == null)
        {
            nextLevel.SetActive(false);
            gameOver.SetActive(true);
        }
    }


    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("Level3");
    }
}
