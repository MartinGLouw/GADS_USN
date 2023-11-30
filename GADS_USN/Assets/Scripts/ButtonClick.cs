using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
   
   public AudioSource source;

   // Use this for initialization
   void Start()
   {
    
   }

   // On button click play sound
   public void ClickSound()
   {
      source.Play();
   }

   public void quit()
   {
      Application.Quit();
   }
}

