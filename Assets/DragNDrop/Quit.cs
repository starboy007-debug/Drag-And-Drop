using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
   public void OnQuit(){
    Application.Quit();
    Debug.Log("quit");
   }
}
