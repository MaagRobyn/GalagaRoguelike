

using UnityEngine;

public abstract class IdentifiableBehavior : MonoBehaviour
{
    protected static int LastID = 0;
    public int Id { get; protected set; }
}