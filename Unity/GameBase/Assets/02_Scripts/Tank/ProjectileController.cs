using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("발사체 오브젝트")]
    private GameObject ProjectileObject;

    public void FireProjectile()
    {
        GameObject temp = (GameObject)Instantiate(ProjectileObject);

        temp.transform.position = this.gameObject.transform.position;

        temp.GetComponent<ProjectileMove>().launchDirection = this.gameObject.transform.forward;

        Destroy(temp, 10f);
    }
}
