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

    member this.Start () =
        if not (isNull this.character) then
            this.maxHitPoints <- this.character.maxHitPoints

    member this.Update () =
        if not (isNull this.character) then
            this.meterImage.fillAmount <- this.hitPoints.value / this.maxHitPoints
            this.hpText.text <- "HP: " + string (this.meterImage.fillAmount * 100.f)