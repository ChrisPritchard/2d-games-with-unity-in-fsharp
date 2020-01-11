namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG.ScriptableObjects

[<AbstractClass>]
type Character() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable startingHitPoints : float32
    [<DefaultValue>]val mutable hitPoints : HitPoints
    [<DefaultValue>]val mutable maxHitPoints : float32

        