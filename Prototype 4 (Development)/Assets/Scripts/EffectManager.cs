using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEffects
{
    none,
    enemyKnockbackHard,
    enemyHeavy,
    playerPushing,
    playerRevive,
}

public class EffectManager : MonoBehaviour
{
    //public Dictionary<EEffects, bool> isEnabled;
    //public Dictionary<EEffects, GameObject> effectGameObject;

    public Buff playerPushing;
    public Buff2 playerRevive;
    public DeBuff enemyKnockbackHard;
    public DeBuff2 enemyHeavy;

    private void Awake()
    {
        //isEnabled = new Dictionary<EEffects, bool>
        //{
        //    { EEffects.none, false },
        //    { EEffects.enemyKnockbackHard, false },
        //    { EEffects.enemyHeavy, false },
        //    { EEffects.playerPushing, false },
        //    { EEffects.playerRevive, false }
        //};
        //effectGameObject = new Dictionary<EEffects, GameObject>
        //{
        //    { EEffects.none, null },
        //    { EEffects.enemyKnockbackHard, null },
        //    { EEffects.enemyHeavy, null },
        //    { EEffects.playerPushing, null },
        //    { EEffects.playerRevive, null }
        //};

        playerPushing = GetComponent<Buff>();
        playerRevive = GetComponent<Buff2>();
        enemyKnockbackHard = GetComponent<DeBuff>();
        enemyHeavy = GetComponent<DeBuff2>();

    }

    // Start is called before the first frame update
    void Start()
    {
        EffectBase.effectManager = this;
        Enemy.effectManager = this;
        PlayerController.effectManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
