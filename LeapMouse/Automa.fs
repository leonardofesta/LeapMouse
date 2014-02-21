module LeapMouse.Automa

open GestIT
open GestIT.FSharp
open LeapMouse.Data
open LeapMouse.Handlers
open LeapMouse.Controller

// costruisce la rete di petri che rappresenta il movimento del mouse
let eventbuilder(cont:LMController) = 
    
    let moving     = new GroundTerm<_,_>(LeapFeatureTypes.Moving)
    let rclickDown = new GroundTerm<_,_>(LeapFeatureTypes.RClickDown)
    let rclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.RClickUp)
    let lclickDown = new GroundTerm<_,_>(LeapFeatureTypes.LClickDown)
    let lclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.LClickUp)
  
    let movement = !*(moving |-> moving_h cont)
    let rightclicks =  !* ((rclickDown |-> rightclickdown_h cont) |>> (rclickUp |-> rightclickup_h cont) )
    let leftclicks  =  !* ((lclickDown |-> leftclickdown_h cont)  |>> (lclickUp |-> leftclickup_h cont) )
    
    let events = (movement |^| leftclicks |^| rightclicks)

    events

// costruisce la rete di petri che rappresenta la fase di calibrazione
let calibrazionebuilder(cont:LMController) = 

    let newfinger   = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger, fun _ -> not (cont.AlreadyCalibrating()))
    let stable1     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
    let stable2     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile2)
    let calibrationend  = new GroundTerm<_,_>(LeapFeatureTypes.Calibrato)

    let calibrating = ((newfinger |-> setcalibratingfinger_h cont) |>> (stable1 |-> standingTL_h  cont ) |>> (stable2 |-> standingLR_h  cont ) |>> (calibrationend |-> nomod_h  cont))
    
    calibrating
