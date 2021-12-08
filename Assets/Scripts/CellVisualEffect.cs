using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellVisualEffect : MonoBehaviour
{

    private void Start()
    {
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = false;

        Cell cell = GetComponentInParent<Cell>();
        cell.OnClickedWrongCell += Shake;
        cell.OnClickedVictoriousCell += Bounce;
        cell.OnClickedVictoriousCell += ActivateParticles;
    }

    internal void SetSprite(Sprite newSprite)
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void Shake() 
    {
        transform.DOShakePosition(1.0f, strength: new Vector3(0.5f, 0, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
    }

    private void Bounce()
    {
        Sequence s = DOTween.Sequence();

        s.Append(transform.DOScale(new Vector3(1.2f, 1.2f, 0), 0.5f));
        //transform.DOScale(new Vector3(0.5f, 0.5f, 0), 0.5f);
        //transform.DOScale(new Vector3(1f, 1f, 0), 0.5f);
        s.SetLoops(2, LoopType.Yoyo);
    }

    private void ActivateParticles() 
    {
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        ParticleSystem.EmissionModule em = particles.emission;
        em.enabled = true;
    }
}
