using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Front : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private float damage;
    [SerializeField] private float timeDamage;
    [SerializeField] private int forceVibration;
    [SerializeField] private float timeVibration;
    [SerializeField] private ParticleSystem[] destroyTrashParticle;


    public float ForceCrusher { get; set; } = 1;
    public float Damage { get => damage; set => damage = value; }

    [SerializeField] private List<DestroyTrash> peacesDestroy = new List<DestroyTrash>();

    private int currentForceVibration;
    private ParticleSystem currentParticaleTrash;

    private float currentResist;
    private float currentStartTimeVibration;
    private float currentTimeDamage = -2f;
    private float currentTimeVibration = -2f;

    private bool damageFlag;
    private bool speedFlag;

    #region Injects

    private SoundManager _soundManager;

    [Inject]
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    #endregion

    private void Start()
    {
        Vibration.Init();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            DestroyTrash destroyTrash = other.gameObject.GetComponent<DestroyTrash>();
            peacesDestroy.Add(destroyTrash);

            if (speedFlag && destroyTrash.Resist != currentResist)
            {
                speedFlag = false;
            }

            if (!speedFlag)
            {
                speedFlag = true;
                currentResist = destroyTrash.Resist / ForceCrusher;

                playerMove.SetResist(currentResist);

                SetSettingsVibration();

                PlayParticleDestroyTrash(destroyTrash);
            }

            StartDamage();
        }
    }

    private void StartDamage()
    {
        if (!damageFlag)
        {
            damageFlag = true;
            
            currentTimeDamage = timeDamage;           
        }
    }

    private void PlayParticleDestroyTrash(DestroyTrash destroyTrash)
    {
        if (currentParticaleTrash != null)
            currentParticaleTrash.Stop();

        currentParticaleTrash = destroyTrashParticle[destroyTrash.GetTypeTrash()];

        currentParticaleTrash.Play();
    }

    private void SetSettingsVibration()
    {
        if (_soundManager.EnableVibro)
        {
            currentStartTimeVibration = timeVibration * currentResist;
            currentForceVibration = (int)(forceVibration * currentResist);
            currentTimeVibration = -0.5f;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            peacesDestroy.Remove(other.gameObject.GetComponent<DestroyTrash>());

            if (damageFlag)
            {
                damageFlag = false;

                currentTimeDamage = -2;
            }
        }
    }

    private void TimerDamage()
    {
        if (currentTimeDamage > 0)
        {
            currentTimeDamage -= Time.deltaTime;
        }
        else if (currentTimeDamage > -1)
        {
            foreach (var item in peacesDestroy.ToArray())
            {
                if (item.gameObject.activeSelf == true)
                {
                    item.DamagePeace((int)(Damage * ForceCrusher));
                }

                if (item.gameObject.activeSelf == false)
                {
                    peacesDestroy.Remove(item);
                }
            }

            currentTimeDamage = timeDamage;
        }
    }

    public void Update()
    {
        TimerDamage();
        TimerVibration();

        if (peacesDestroy.Count == 0 && speedFlag)
        {
            speedFlag = false;
            playerMove.SetNormalSpeed();
            currentTimeVibration = -2;
            currentParticaleTrash.Stop();
        }
    }

    private void TimerVibration()
    {
        if (_soundManager.EnableVibro)
        {
            if (currentTimeVibration > 0)
            {
                currentTimeVibration -= Time.deltaTime;
            }
            else if (currentTimeVibration > -1)
            {
                Vibration.Vibrate(currentForceVibration);
                currentTimeVibration = currentStartTimeVibration;
            }
        }
    }
}
