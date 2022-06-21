using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteObstacle : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    public void Change()
    {
        Destroy(_collider);
    }
}
