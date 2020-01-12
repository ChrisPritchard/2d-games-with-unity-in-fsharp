namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG.ScriptableObjects

type Enemy() = 
    inherit Character()

    [<DefaultValue>]val mutable hitPoints : float32