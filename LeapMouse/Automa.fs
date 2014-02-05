module LeapMouse.Automa

open GestIT
open GestIT.FSharp
open LeapMouse.Data
open LeapMouse.Handlers
open LeapMouse.Controller

let eventbuilder( app:_,cont:LMController) = 
    
 //   let nuovodito  = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger,fun _ -> not (cont.AlreadyCalibrating()))
 //   let fermo      = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
 //   let fermo2     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile2)
    let moving     = new GroundTerm<_,_>(LeapFeatureTypes.Moving)
 //   let stopmodify = new GroundTerm<_,_>(LeapFeatureTypes.Calibrato)
    let lclick     = new GroundTerm<_,_>(LeapFeatureTypes.LClick)
    let rclickDown = new GroundTerm<_,_>(LeapFeatureTypes.RClickDown)
    let rclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.RClickUp)
    let lclickDown = new GroundTerm<_,_>(LeapFeatureTypes.LClickDown)
    let lclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.LClickUp)


 //   let calibrating = ((nuovodito |-> setcalibratingfinger_h app cont) |>> (fermo |-> standingTL_h app cont ) |>> (fermo2 |-> standingLR_h app cont ) |>> (stopmodify |-> nomod_h app cont))
  
    let movement = !*(moving |-> moving_h cont)
    let rightclicks =  !* ((rclickDown |-> rightclickdown_h cont) |>> (rclickUp |-> rightclickup_h cont) )
    let leftclicks  =  !* ((lclickDown |-> leftclickdown_h cont)  |>> (lclickUp |-> leftclickup_h cont) )
    
    let events = // calibrating |>> 
                 (movement |^| leftclicks |^| rightclicks)

    events

let calibrazionebuilder(cont:LMController) = 

    let newfinger   = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger,fun _ -> System.Console.WriteLine("calibrazione boh " + cont.AlreadyCalibrating().ToString())
                                                                              not (cont.AlreadyCalibrating()))
    let stable1     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
    let stable2     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile2)
    let calibrationend  = new GroundTerm<_,_>(LeapFeatureTypes.Calibrato)

    let calibrating = ((newfinger |-> setcalibratingfinger_h cont) |>> (stable1 |-> standingTL_h  cont ) |>> (stable2 |-> standingLR_h  cont ) |>> (calibrationend |-> nomod_h  cont))
    
    calibrating
