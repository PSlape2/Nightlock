using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector2> Position = new NetworkVariable<Vector2>();

        new Rigidbody2D rigid;

        public float moveSpeed = 1000f;

        public override void OnNetworkSpawn()
        {
            
            if (IsOwner)
            {
                Move();
            }
        }
        public void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
        public void Move()
        {
            if (NetworkManager.Singleton.IsClient)
            {
                //Debug.Log("Is Client");
                Position.Value = getMove();
                Debug.Log(Position.Value);
            }
        }
        /*
        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetMove();
        }
        
        static Vector2 GetRandomPosition() 
        {
            return new Vector2(
                Random.Range(-3f, 3f), 
                Random.Range(-3f, 3f)
            );
        }
        */

        static Vector2 getMove()
        {
            //Debug.Log("got moved");
            return new Vector2(
                Input.GetAxis("Horizontal"), 
                Input.GetAxis("Vertical")
            );
        }

    [ServerRpc]
        void updateVelocityServerRpc(Vector2 movement) {
            Position.Value = movement;
        }

        void Update()
        {
            if(IsOwner) {
                updateVelocityServerRpc(getMove());
            }
            rigid.velocity = (Position.Value * moveSpeed) * Time.deltaTime;
        }
    }
}