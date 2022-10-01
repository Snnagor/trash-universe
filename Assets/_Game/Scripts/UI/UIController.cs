using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{       
    [Header("UI Canvases")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject pausePanel;    
    [SerializeField] private GameObject shopPanel;    
    [SerializeField] private GameObject comingSoonPanel;

    private GameObject currentPanel;


    private void Start()
    {
        GamePanel();
    }
   
    private void ChangeState(GameObject state)
    {
        if (currentPanel != null)
        {
            if (currentPanel != gamePanel)
                currentPanel.SetActive(false);
        }
             
        currentPanel = state;

        if (currentPanel != null)
            currentPanel.SetActive(true);
    }

    #region Panels
   
    public void GamePanel()
    {
        Time.timeScale = 1;
        ChangeState(gamePanel);
    }

    public void PausePanel()
    {
        Time.timeScale = 0;
        ChangeState(pausePanel);       
    }

    #endregion

    public void ShopPanel()
    {
        Time.timeScale = 0;
        ChangeState(shopPanel);
    }

    public void ComingSoonPanel()
    {
        Time.timeScale = 0;
        ChangeState(comingSoonPanel);
    }

}
