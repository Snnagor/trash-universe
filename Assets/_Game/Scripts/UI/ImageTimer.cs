using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ImageTimer : MonoBehaviour
{
    [SerializeField] private Image iconBonus;   

    public bool Enable { get; set; } = false;
    public float MaxTime { get; set; }

    private float currentTime = -2;

    #region Injects

    private BonusesManager _bonusManager;

    [Inject]
    private void Construct(BonusesManager bonusManager)
    {
        _bonusManager = bonusManager;
    }

    #endregion

    public void ReseTime()
    {
        currentTime = MaxTime;
    }

    public void StartTimer(float time)
    {
        MaxTime = time;
        currentTime = MaxTime;
        Enable = true;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            if (!_bonusManager.PauseBonus)
            {
                currentTime -= Time.deltaTime;
                iconBonus.fillAmount = currentTime / MaxTime;
            }           
        }
        else if (currentTime > -1)
        {
            Enable = false;
            gameObject.SetActive(false);
            currentTime = -2;
        }
    }
}
