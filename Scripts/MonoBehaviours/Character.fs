namespace HalpernRPG.MonoBehaviours

open UnityEngine

[<AbstractClass>]
type Character() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable hitPoints : float32
    [<DefaultValue>]val mutable maxHitPoints : float32

        