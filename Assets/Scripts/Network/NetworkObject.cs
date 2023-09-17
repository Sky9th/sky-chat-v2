using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sky9th.Network
{    public class NetworkObject : MonoBehaviour
    {

        protected NetworkManager networkManager;

        // Start is called before the first frame update
        void Start()
        {
            networkManager = GameObject.FindFirstObjectByType<NetworkManager>().GetComponent<NetworkManager>();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
