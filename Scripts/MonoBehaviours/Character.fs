namespace HalpernRPG.MonoBehaviours

open UnityEngine

[<AbstractClass>]
type Character() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable hitPoints : int
    [<DefaultValue>]val mutable maxHitPoints : int

        