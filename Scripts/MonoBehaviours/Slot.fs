namespace HalpernRPG.MonoBehaviours

open UnityEngine
open UnityEngine.UI

type Slot() =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable qtyText : Text