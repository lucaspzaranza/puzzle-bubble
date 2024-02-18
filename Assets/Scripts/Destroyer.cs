using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Projectile")
            Destroy(other.gameObject);
    }
}
