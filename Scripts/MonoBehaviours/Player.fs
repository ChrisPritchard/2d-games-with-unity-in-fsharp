namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG.ScriptableObjects

type Player() as this = 
    inherit Character()

    let adjustHitPoints amount =
        let newValue = max 0.f (min (this.hitPoints.value + float32 amount) this.maxHitPoints)
        if newValue = this.hitPoints.value then false
        else
            this.hitPoints.value <- newValue
            true

    member __.Start () =
        __.hitPoints.value <- __.startingHitPoints

    member __.OnTriggerEnter2D (collision: Collider2D) =
        if collision.gameObject.CompareTag "CanBePickedUp" then
            let hitObject = collision.gameObject.GetComponent<Consumable>().item
            if not (isNull hitObject) then
                Debug.Log ("it: " + hitObject.objectName)

                let removeOther =
                    match hitObject.itemType with
                    | ItemType.HEALTH ->
                        adjustHitPoints hitObject.quantity
                    | _ -> true
                if removeOther then
                    collision.gameObject.SetActive false