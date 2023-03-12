using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

namespace Screens
{

    public enum ScreenType
    {
        Main,
        LevelSelect,
        Credits,
        Shop
    }


    public class ScreenBase : MonoBehaviour
    {
        public ScreenType screenType;
        public List<Transform> listOfObjects;

        public bool startHided = false;

        [Header("Animation")]
        public float animationDuration = 0.3f;
        public float delayBetweenObjects = 0.05f;

        private void Start()
        {
            if (startHided)
            {
                HideObjects();
            }
        }


        [Button]
        protected virtual void Show()
        {
            ShowObjects();
            Debug.Log("Show");
        }

        [Button]
        protected virtual void Hide()
        {
            HideObjects();
            Debug.Log("Hide");
        }

        private void HideObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(false));
        }

        private void ShowObjects()
        {
            for (int i = 0; i < listOfObjects.Count; i++)
            {
                var obj = listOfObjects[i];
                obj.gameObject.SetActive(true);
                obj.DOScale(0, animationDuration).From().SetDelay(i * delayBetweenObjects);
            }

        }

        private void ForceShowObjects()
        {
            listOfObjects.ForEach(i => i.gameObject.SetActive(true));

        }
    }

}