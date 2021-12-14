using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cutscenes {

    public interface ICutsceneAction {

        event Action<ICutsceneAction> OnStarted;
        event Action<ICutsceneAction> OnEnded;

        void Start ();
        void PlaySimultaneousActions ();
        //void Pause();
    }
}