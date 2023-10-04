using Sky9th.Network;
using Sky9th.Protobuf;
using UnityEngine;

public class GamePlayer : GameObject
{
    public float speed;
    public string netowrkId;

    private Animator animator;

    private Vector3 nextPosition;
    float t = 0f;

    private Sky9th.Protobuf.Transform sendTrans;
    private Sky9th.Protobuf.Transform recTrans;

    private void Start()
    {
        Inject();
        animator = GetComponent<Animator>();
        sendTrans = new()
        {
            X = transform.position.x,
            Y = transform.position.y,
            Z = transform.position.z,
            Up = false,
            Down = false,
            Left = false,
            Right = false
        };
    }


    private void Update()
    {
        if (gameObject.GetComponent<NetworkObject>().isLocalPlayer)
        {
            sendTrans.Up = false;
            sendTrans.Down = false;
            sendTrans.Left = false;
            sendTrans.Right = false;

            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                sendTrans.Left = true;
                sendTrans.Right = false;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                sendTrans.Left = false;
                sendTrans.Right = true;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                sendTrans.Up = true;
                sendTrans.Down = false;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                sendTrans.Up = false;
                sendTrans.Down = true;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;

            sendTrans.X = transform.position.x;
            sendTrans.Y = transform.position.y;
            sendTrans.Z = transform.position.z;
        }
        else if (recTrans != null && transform.position != nextPosition)
        {
            Vector2 dir = nextPosition - transform.position;
            Debug.Log(recTrans);
            if (recTrans.Left)
            {
                animator.SetInteger("Direction", 3);
            }
            else if (recTrans.Right)
            {
                animator.SetInteger("Direction", 2);
            }

            if (recTrans.Up)
            {
                animator.SetInteger("Direction", 1);
            }
            else if (recTrans.Down)
            {
                animator.SetInteger("Direction", 0);
            }
            animator.SetBool("IsMoving", dir.magnitude > 0);
            transform.position = Vector3.Lerp(transform.position, nextPosition, speed * Time.deltaTime);
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
                Transform = sendTrans
            };
            networkWriter.Send(playerInfo);
        }
    }

    public override void NetworkDataHandler(byte[] msg, string method)
    {
        //Debug.Log("GamePlayer NetworkDataHandler:" + msg.Length + ":" + method);
        switch(method)
        {
            case "ParsePlayerInfo":
                PlayerInfo playerInfo = PlayerInfo.Parser.ParseFrom(msg);
                if (!isLocalPlayer && playerInfo.NetworkID == netowrkId)
                {
                    recTrans = playerInfo.Transform;
                    nextPosition = new Vector3((float)playerInfo.Transform.X, (float)playerInfo.Transform.Y, 0);
                }
                break;
        }
    }



}
