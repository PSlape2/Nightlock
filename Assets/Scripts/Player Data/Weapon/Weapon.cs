using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon : NetworkBehaviour
{
    // Start is called before the first frame update

    private Transform firePoint;
    public GameObject bulletPrefab;
    private Transform player;

    void Start() {
        player = GetComponent<Transform>();
        firePoint = player.GetChild(0);
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetButtonDown("Fire1")) {
            if(IsOwner) {
                onFireServerRpc();
            }
        }
    }
    [ServerRpc]
    void onFireServerRpc(ServerRpcParams serverRpcParams = default) {
        GameObject go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        go.GetComponent<NetworkObject>().SpawnWithOwnership(serverRpcParams.Receive.SenderClientId);
    }
}
