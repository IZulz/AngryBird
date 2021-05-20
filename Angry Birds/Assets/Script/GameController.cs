using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShoot SlingShoot;
    public TrailController TrailController;

    private Birds _shotBird;
    public BoxCollider2D TapCollider;

    public List<Birds> Birds;
    public List<Enemy> Enemies;

    public GameObject btn;

    private bool _isGameEnded = false;

    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        SlingShoot.InitiateBirds(Birds[0]);

        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        _shotBird = Birds[0];
    }

    public void AssignTrail(Birds bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;
    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }

    public void ChangeBird()
    {
        TapCollider.enabled = false;

        if (_isGameEnded)
        {
            return;
        }


        Birds.RemoveAt(0);

        if (Birds.Count > 0)
        {
            SlingShoot.InitiateBirds(Birds[0]);
            _shotBird = Birds[0];
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i].gameObject == destroyedEnemy)
            {
                Enemies.RemoveAt(i);
                break;
            }
        }

        if (Enemies.Count == 0)
        {
            _isGameEnded = true;

            Debug.Log("Win game");

            if(btn != null)
            {
                btn.SetActive(true);
            }
        }
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Main 2");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
