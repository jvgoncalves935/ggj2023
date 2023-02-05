using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCutscene : MonoBehaviour
{
    public int id;
    [SerializeField] private string[] textosCutscenes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(IsCorpoJogador(other)) {
            ImageCutsceneUIController.InstanciaImageCutsceneUIController.CutsceneFinalCoroutine(textosCutscenes);
        }
    }

    public bool IsCorpoJogador(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }

    
}
