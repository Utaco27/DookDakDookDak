using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Singleton
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public bool is_topView = false;

    public void ChangeGameView()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (is_topView)
            {
                is_topView = false;
            }
            else if (!is_topView)
            {
                is_topView = true;
            }
        }
    }

    private void Update()
    {
        ChangeGameView();
    }
}
