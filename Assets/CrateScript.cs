using Assets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CrateScript : MonoBehaviour
{
    public Reward reward;

    private float health = 25;
    [SerializeField] private TextMeshPro titleObj;
    [SerializeField] private TextMeshPro descriptionObj;
    [SerializeField] private SpriteRenderer statusBar;
    private const int MAX_HEALTH = 25;



    private void Start()
    {
        health = MAX_HEALTH;
        titleObj.text = reward.title;
        descriptionObj.text = reward.description;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log($"Health: {health}, Damage: {damage}");
        health -= damage;
        if(health < 0)
        {
            statusBar.color = Color.black;
            health = int.MaxValue;
            GameManager.Instance.Player.newRewards.Enqueue(reward);
            InvokeOnBoxDestroyed();
        }
        else
        {
            var scale = health / MAX_HEALTH;
            statusBar.color = Color.Lerp(Color.green, Color.red, scale);

        }
    }

    public delegate void DestroyBox();
    public event DestroyBox OnBoxDestroyed;
    protected virtual void InvokeOnBoxDestroyed()
    {
        OnBoxDestroyed?.Invoke();
    }
}
