using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private FirstPersonController firstPersonController;
    // Start is called before the first frame update

    [SerializeField] public static GameObject instanciaPlayer;
    private static Player _instanciaPlayer;
    public static Player InstanciaPlayer {
        get {
            if(_instanciaPlayer == null) {
                _instanciaPlayer = instanciaPlayer.GetComponent<Player>();
            }
            return _instanciaPlayer;
        }
    }
    void Awake() {
        instanciaPlayer = FindObjectOfType<Player>().gameObject;
    }

    void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFirstPersonController(bool flag) {
        firstPersonController.enabled = flag;
    }
}
