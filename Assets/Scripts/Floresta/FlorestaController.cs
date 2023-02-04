using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorestaController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
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
}
