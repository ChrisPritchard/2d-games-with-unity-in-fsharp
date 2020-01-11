namespace HalpernRPG

open UnityEngine
open Cinemachine

type RoundCameraPos() as this =
    inherit CinemachineExtension()

    [<DefaultValue>]val mutable pixelsPerUnit : float32

    do
        this.pixelsPerUnit <- 32.f

    override __.PostPipelineStageCallback (_, stage, state, _) =
        
        let round x = Mathf.Round (x * __.pixelsPerUnit) / __.pixelsPerUnit
        if stage = CinemachineCore.Stage.Body then
            let pos = state.FinalPosition
            let pos2 = Vector3 (round pos.x, round pos.y, pos.z)
            state.PositionCorrection <- state.PositionCorrection + (pos2 - pos)