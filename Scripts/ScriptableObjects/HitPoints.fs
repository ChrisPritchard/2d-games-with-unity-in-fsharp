namespace HalpernRPG.ScriptableObjects

open UnityEngine

[<CreateAssetMenu(menuName = "HitPoints")>]
[<AllowNullLiteral>]
type HitPoints() =
    inherit ScriptableObject()
    
    [<DefaultValue>]val mutable value : float32