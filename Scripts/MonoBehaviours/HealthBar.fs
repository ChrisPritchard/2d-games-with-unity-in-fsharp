namespace HalpernRPG.MonoBehaviours

open UnityEngine
open UnityEngine.UI
open HalpernRPG.ScriptableObjects

type HealthBar() =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable hitPoints : HitPoints
    [<DefaultValue>][<HideInInspector>]val mutable character : Character
    [<DefaultValue>]val mutable meterImage : Image
    [<DefaultValue>]val mutable hpText : Text
    [<DefaultValue>]val mutable maxHitPoints : float32

    member __.Start () =
        if not (isNull __.character) then
            __.maxHitPoints <- __.character.maxHitPoints

    member __.Update () =
        if not (isNull __.character) then
            __.meterImage.fillAmount <- __.hitPoints.value / __.maxHitPoints
            __.hpText.text <- "HP: " + string (__.meterImage.fillAmount * 100.f)