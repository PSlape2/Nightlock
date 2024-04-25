using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class FaceMouse : NetworkBehaviour
{
    NetworkVariable<Vector2> direction = new NetworkVariable<Vector2>();

    new Rigidbody2D rigid;

    void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        faceMouse();
        rigid.transform.up = new Vector3(direction.Value.x, direction.Value.y, 0);
    }

    void faceMouse() {
        if(NetworkManager.Singleton.IsClient && Application.isFocused) {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 rota = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        if(IsOwner) {
            updateRotationServerRpc(rota);
        }
        

        }
        
       
    }

    [ServerRpc]
    void updateRotationServerRpc(Vector2 rot) 
    {
        direction.Value = rot;
    }
}
