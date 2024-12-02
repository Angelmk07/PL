using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Source.Game
{
    public class Start : MonoBehaviour
    {
        public void LoadSceen()
        {
            SceneManager.LoadScene(1);
        }
    }
}