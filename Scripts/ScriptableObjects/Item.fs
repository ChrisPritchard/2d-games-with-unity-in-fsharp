namespace HalpernRPG.ScriptableObjects

open UnityEngine

type ItemType =
    | COIN = 0
    | HEALTH = 1

[<CreateAssetMenu(menuName = "Item")>]
[<AllowNullLiteral>]
type Item() =
    inherit ScriptableObject()
    
    [<DefaultValue>]val mutable objectName : string
    [<DefaultValue>]val mutable sprite : Sprite
    [<DefaultValue>]val mutable quantity : int
    [<DefaultValue>]val mutable stackable : bool
    [<DefaultValue>]val mutable itemType : ItemType