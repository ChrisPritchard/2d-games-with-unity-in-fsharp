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
    
    let mutable maxHitPoints = 0.f

    member this.Start () =
        if not (isNull this.character) then
            maxHitPoints <- this.character.maxHitPoints

    member this.Update () =
        if not (isNull this.character) then
            this.meterImage.fillAmount <- this.hitPoints.value / maxHitPoints
            this.hpText.text <- "HP: " + string (this.meterImage.fillAmount * 100.f)