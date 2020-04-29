using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectController : MonoBehaviour {

    #region 单例
    private static EffectController instance;
    
    public static EffectController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("EffectController").GetComponent<EffectController>();
            return instance;
        }
    }
    #endregion

    public GameObject goldEffect;
    public GameObject starEffect;
    public GameObject seaWaveEffect;
    

    private ParticleSystem goldParticle;
    private ParticleSystem starParticle;

    void Start()
    {
        goldParticle = goldEffect.GetComponent<ParticleSystem>();
        starParticle = starEffect.GetComponent<ParticleSystem>();
    }

    public void GoldEffectHappen(Transform GoldShark)
    {
        goldEffect.SetActive(true);
        goldEffect.transform.position = GoldShark.position;
        Invoke("DestroyGoldEffect", goldParticle.main.startLifetime.constant / goldParticle.main.simulationSpeed);
    }

    private void DestroyGoldEffect()
    {
        goldEffect.SetActive(false);
    }

    public void StarEffectHappen()
    {
        starEffect.SetActive(true);
        Invoke("DestroyStarEffect", starParticle.main.startLifetime.constant / starParticle.main.simulationSpeed);
    }

    private void DestroyStarEffect()
    {
        starEffect.SetActive(false);
    }

    public void StartSeaWaveEffect()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.changeBackgroundAudio);
        seaWaveEffect.SetActive(true);
    }
}
