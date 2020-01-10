namespace HalpernRPG

open UnityEngine

type MovementController() = 
    inherit MonoBehaviour()

    // Start is called before the first frame update
    member _.Start() = Debug.Log("F# Started with watch and msbuild copy")

    // Update is called once per frame
    member _.Update() = ()
