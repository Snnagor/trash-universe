using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Zenject;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image loadingImg;
    [SerializeField] private Image BackgroundPanel;
    [SerializeField] private Sprite[] backgroundsSprite;
   
    AsyncOperation async;

    private void Start()
    {
        BackgroundPanel.sprite = GetRandomBackground();

        Invoke("BeginLoading", 0.5f);
    }

    public void BeginLoading()
    {        
        StartCoroutine(LaunchLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LaunchLevel(int indexScene)
    {        
        async = SceneManager.LoadSceneAsync(indexScene);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            loadingImg.fillAmount = async.progress;
             
            yield return new WaitForSeconds(0.01f);
        }

        loadingImg.fillAmount = 1f;
        
    }

    private void Update()
    {
        if (async == null) return;

        if (async.progress == 0.9f )
        {
            async.allowSceneActivation = true;
        }
    }

    private Sprite GetRandomBackground()
    {
        return backgroundsSprite[Random.Range(0, backgroundsSprite.Length)];
    }
}
