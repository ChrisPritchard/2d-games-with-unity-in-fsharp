namespace HalpernRPG

open UnityEngine

type MovementController = 
    inherit MonoBehaviour

    val mutable movementSpeed : float32

    new() = {
        movementSpeed = float32 3.
    }

    member this.Start() = 
        let message = System.String.Format ("F# Movement Speed: {0}", this.movementSpeed)
        Debug.Log message