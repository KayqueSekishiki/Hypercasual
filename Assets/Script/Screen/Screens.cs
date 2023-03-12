using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Screens
{

    public enum ScreenType
    {
        Main,
        LevelSelect,
        Credits,
        Shop
    }


    public class Screens : MonoBehaviour
    {
        public ScreenType screenType;
        public List<Transform> listOfObjects;


        protected virtual void Show()
        {
            Debug.Log("Show");
        }
    }

}