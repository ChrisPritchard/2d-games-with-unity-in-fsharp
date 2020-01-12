namespace HalpernRPG.MonoBehaviours

open System.Collections
open UnityEngine
open HalpernRPG.ScriptableObjects

[<AbstractClass>]
[<AllowNullLiteral>]
type Character() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable startingHitPoints : float32
    [<DefaultValue>]val mutable maxHitPoints : float32

    abstract member KillCharacter: unit -> unit
    default this.KillCharacter () =
        MonoBehaviour.Destroy this.gameObject

    abstract member ResetCharacter: unit -> unit

    abstract member DamageCharacter: float32 -> float32 -> IEnumerator
    
