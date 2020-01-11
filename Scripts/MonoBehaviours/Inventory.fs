namespace HalpernRPG.MonoBehaviours

open UnityEngine
open UnityEngine.UI
open HalpernRPG.ScriptableObjects

type Inventory() as this = 
    inherit MonoBehaviour()

    let numSlots = 5

    [<DefaultValue>]val mutable slotPrefab : GameObject
    [<DefaultValue>]val mutable itemImages : Image[]
    [<DefaultValue>]val mutable items : Item[]
    [<DefaultValue>]val mutable slots : GameObject[]

    do
        this.itemImages <- Array.zeroCreate numSlots
        this.items <- Array.zeroCreate numSlots
        this.slots <- Array.zeroCreate numSlots

    let createSlots () =
        if isNull this.slotPrefab then ()

        for i = 0 to numSlots - 1 do
            let newSlot = MonoBehaviour.Instantiate this.slotPrefab :?> GameObject
            newSlot.name <- "ItemSlot_" + string i
            newSlot.transform.SetParent (this.gameObject.transform.GetChild(0).transform)
            this.slots.[i] <- newSlot
            this.itemImages.[i] <- newSlot.transform.GetChild(1).GetComponent<Image>()

    member this.AddItem (itemToAdd: Item) =
        let index = this.items |> Array.tryFindIndex (fun item -> 
            isNull item || (item.itemType = itemToAdd.itemType && itemToAdd.stackable))

        match index with
        | Some i ->
            if isNull this.items.[i] then
                this.items.[i] <- MonoBehaviour.Instantiate itemToAdd :?> Item
                this.items.[i].quantity <- 1
                this.itemImages.[i].sprite <- itemToAdd.sprite
                this.itemImages.[i].enabled <- true
            else
                let newQuantity = this.items.[i].quantity + 1
                this.items.[i].quantity <- newQuantity
                let slot = this.slots.[i].gameObject.GetComponent<Slot>()
                slot.qtyText.enabled <- true
                slot.qtyText.text <- string newQuantity
            true
        | _ -> false

    member __.Start () =
        createSlots ()