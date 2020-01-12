namespace HalpernRPG.MonoBehaviours

open System.Collections
open UnityEngine
open HalpernRPG.ScriptableObjects

type Player() as this = 
    inherit Character()

    [<DefaultValue>]val mutable hitPoints : HitPoints
    [<DefaultValue>]val mutable healthBarPrefab : HealthBar
    [<DefaultValue>]val mutable inventoryPrefab : Inventory

    let mutable healthBar = Unchecked.defaultof<HealthBar>
    let mutable inventory = Unchecked.defaultof<Inventory>
    
    let adjustHitPoints amount =
        let newValue = max 0.f (min (this.hitPoints.value + amount) this.maxHitPoints)
        if newValue = this.hitPoints.value then false
        else
            this.hitPoints.value <- newValue
            true

    member this.OnEnable () =
        this.ResetCharacter ()

    member _.OnTriggerEnter2D (collision: Collider2D) =
        if collision.gameObject.CompareTag "CanBePickedUp" then
            let hitObject = collision.gameObject.GetComponent<Consumable>().item
            if not (isNull hitObject) then
                Debug.Log ("it: " + hitObject.objectName)

                let removeOther =
                    match hitObject.itemType with
                    | ItemType.HEALTH ->
                        adjustHitPoints (float32 hitObject.quantity)
                    | ItemType.COIN -> 
                        inventory.AddItem hitObject
                    | _ -> 
                        false
                if removeOther then
                    collision.gameObject.SetActive false

    override this.ResetCharacter () =
        healthBar <- MonoBehaviour.Instantiate (this.healthBarPrefab) :?> HealthBar
        inventory <- MonoBehaviour.Instantiate (this.inventoryPrefab) :?> Inventory
        healthBar.character <- this
        this.hitPoints.value <- this.startingHitPoints

    override _.KillCharacter () =
        base.KillCharacter ()
        MonoBehaviour.Destroy healthBar.gameObject
        MonoBehaviour.Destroy inventory.gameObject

    override this.DamageCharacter damage interval = 
        seq {
            let mutable shouldBreak = false
            while not shouldBreak do
                this.StartCoroutine (this.FlickerCharacter ()) |> ignore
                adjustHitPoints -damage |> ignore
                if this.hitPoints.value <= 0.f then
                    this.KillCharacter ()
                    shouldBreak <- true
                else if interval > 0.f then
                    yield WaitForSeconds interval
                else 
                    shouldBreak <- true
        } :?> IEnumerator