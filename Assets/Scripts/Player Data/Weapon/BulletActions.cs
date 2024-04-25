using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using PlayerStuff;

namespace Weapons {
    public class BulletActions : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public NetworkObject netObj;
    public ContactPoint2D[] colList = new ContactPoint2D[6];
    public int contactCount;
    public string curTag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        netObj = GetComponent<NetworkObject>();

        rb.velocity = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        try {
            contactCount = rb.GetContacts(colList);
        }
        catch {
            contactCount = 0;
        }
        finally {
            if(contactCount != 0) {
                foreach(ContactPoint2D col in colList) 
                {
                    try {
                        GameObject collideItem = col.collider.gameObject;
                        curTag = collideItem.tag;
                    }
                    catch {
                        curTag = "None";
                    }
                    finally {
                        if(curTag == "Environment") 
                        {
                            despawnObjectServerRpc();
                        }
                        if(curTag == "Player") {
                            PlayerStats.healthUpdate(-1);
                            despawnObjectServerRpc();
                        }
                    }      
                }
            }
        }
    }

    [ServerRpc]
    void despawnObjectServerRpc() {
        netObj.Despawn();
    }
}

}
