using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorestaController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] public static GameObject instanciaFlorestaController;
    private static FlorestaController _instanciaFlorestaController;
    public static FlorestaController InstanciaFlorestaController {
        get {
            if(_instanciaFlorestaController == null) {
                _instanciaFlorestaController = instanciaFlorestaController.GetComponent<FlorestaController>();
            }
            return _instanciaFlorestaController;
        }
    }
    void Awake() {
        instanciaFlorestaController = FindObjectOfType<FlorestaController>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        MusicaInicio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Nami Budi");
    }
}
