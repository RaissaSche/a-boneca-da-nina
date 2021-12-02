using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMom : MonoBehaviour
{
   public bool hasPassed = false;

public bool getHasPassed(){
       return hasPassed;
   }

   public void setHasPassed(bool hasPassed){
       this.hasPassed = hasPassed;
   }
}
