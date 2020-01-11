namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG.ScriptableObjects

type Player() as this = 
    inherit Character()

    let adjustHitPoints amount =
        this.hitPoints <- max 0 (min (this.hitPoints + amount) this.maxHitPoints)
        Debug.Log (System.String.Format("added {0}. new hitpoints is {1}", amount, this.hitPoints))

    member __.OnTriggerEnter2D (collision: Collider2D) =
        if collision.gameObject.CompareTag "CanBePickedUp" then
            let hitObject = collision.gameObject.GetComponent<Consumable>().item
            if not (isNull hitObject) then
                Debug.Log ("it: " + hitObject.objectName)

                match hitObject.itemType with
                | ItemType.HEALTH ->
                    adjustHitPoints hitObject.quantity
                | _ -> ()
                collision.gameObject.SetActive false