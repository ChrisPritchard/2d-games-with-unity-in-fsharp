namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG.ScriptableObjects

type Consumable() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable item : Item

        