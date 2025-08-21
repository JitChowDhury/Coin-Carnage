using System.Collections;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


}
