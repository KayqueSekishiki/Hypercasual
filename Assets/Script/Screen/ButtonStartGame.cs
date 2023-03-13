using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStartGame : MonoBehaviour
{
    public ParticleSystem myParticleSystem;

    private GameObject _myCamera;

    private void Start()
    {
        _myCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void OnClick(int sceneIndex)
    {
        myParticleSystem.gameObject.transform.SetParent(_myCamera.transform);
        myParticleSystem.gameObject.transform.localPosition = new Vector3(0, 0, 7);
        myParticleSystem.gameObject.transform.localScale = Vector3.one;
        myParticleSystem.Play();
        StartCoroutine(Load(sceneIndex));
    }

    IEnumerator Load(int sceneIndex)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }


}
