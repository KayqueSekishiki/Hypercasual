using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typper : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float timeBetweenLetters = .1f;

    public string phrase;


    private void Awake()
    {
        textMesh.text = "";
    }

    private void OnDisable()
    {
        textMesh.text = "";
    }

    [NaughtyAttributes.Button]
    public void StartType()
    {
        StartCoroutine(Type(phrase));
    }

    IEnumerator Type(string s)
    {
        textMesh.text = "";
        foreach (var l in s.ToCharArray())
        {
            textMesh.text += l;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }
}
