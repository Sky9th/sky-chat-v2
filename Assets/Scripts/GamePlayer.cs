using Sky9th.Network;
using Sky9th.Protobuf;
using UnityEngine;

public class GamePlayer : GameObject
{
    public float speed;
    public string netowrkId;

    private Animator animator;

    private void Start()
    {
        Inject();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (gameObject.GetComponent<NetworkObject>().isLocalPlayer)
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
    }


    private void FixedUpdate()
    {
        if (networkIdentify != null)
        {
            netowrkId = GetComponent<NetworkObject>().networkIdentify.ToString();
        }
        if (networkIdentify != null && isLocalPlayer)
        {
            PlayerInfo playerInfo = new()
            {
                NetworkID = networkIdentify.ToString(),
                Type = "PlayerInfo",
                Transform = new Sky9th.Protobuf.Transform()
                {
                    X = transform.position.x,
                    Y = transform.position.y,
                    Z = transform.position.z
                }
            };
            networkWriter.Send(playerInfo);
        }
    }



}
