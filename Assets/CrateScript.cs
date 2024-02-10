using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrateScript : MonoBehaviour
{
    public Reward reward;

    private float health = 25;
    private TextMeshPro titleObj;
    private TextMeshPro descriptionObj;



    private void Start()
    {
        titleObj.text = reward.title;
        descriptionObj.text = reward.description;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health < 0)
        {
            health = int.MaxValue;
            GameManager.Instance.Player.newRewards.Enqueue(reward);
            InvokeOnBoxDestroyed();
        }
    }

    public delegate void DestroyBox();
    public event DestroyBox OnBoxDestroyed;
    protected virtual void InvokeOnBoxDestroyed()
    {
        OnBoxDestroyed?.Invoke();
    }
}
