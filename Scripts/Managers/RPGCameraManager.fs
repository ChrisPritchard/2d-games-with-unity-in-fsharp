namespace HalpernRPG.Managers

open UnityEngine
open Cinemachine

[<AllowNullLiteral>]
type RPGCameraManager() =
    inherit MonoBehaviour()

    [<DefaultValue>][<HideInInspector>]val mutable virtualCamera : CinemachineVirtualCamera

    static let mutable sharedInstance = Unchecked.defaultof<_>

    member this.Awake () =
        if sharedInstance <> this && not (isNull sharedInstance) then
            MonoBehaviour.Destroy this.gameObject
        else
            sharedInstance <- this
        
        let camera = GameObject.FindWithTag "VirtualCamera"
        this.virtualCamera <- camera.GetComponent<CinemachineVirtualCamera>()