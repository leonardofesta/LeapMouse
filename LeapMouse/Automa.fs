module LeapMouse.Automa

open GestIT
open GestIT.FSharp
open LeapMouse.Data
open LeapMouse.Handlers
open LeapMouse.Controller

let eventbuilder( app:_,cont:LMController) = 
    
    let nuovodito  = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger,fun _ -> not (cont.AlreadyCalibrating()))
    let fermo      = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
    let fermo2     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile2)
    let moving     = new GroundTerm<_,_>(LeapFeatureTypes.Moving)
    let stopmodify = new GroundTerm<_,_>(LeapFeatureTypes.Calibrato)
    let lclick     = new GroundTerm<_,_>(LeapFeatureTypes.LClick)
    let rclickDown = new GroundTerm<_,_>(LeapFeatureTypes.RClickDown)
    let rclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.RClickUp)
    let lclickDown = new GroundTerm<_,_>(LeapFeatureTypes.LClickDown)
    let lclickUp   = new GroundTerm<_,_>(LeapFeatureTypes.LClickUp)


    let calibrating = ((nuovodito |-> setcalibratingfinger_h app cont) |>> (fermo |-> standingTL_h app cont ) |>> (fermo2 |-> standingLR_h app cont ) |>> (stopmodify |-> nomod_h app cont))
  
    let movement = !*(moving |-> moving_h cont)
    let rightclicks =  !* ((rclickDown |-> rightclickdown_h cont) |>> (rclickUp |-> rightclickup_h cont) )
    let leftclicks  =  !* ((lclickDown |-> leftclickdown_h cont)  |>> (lclickUp |-> leftclickup_h cont) )
    
    let events = calibrating |>> (movement |^| leftclicks |^| rightclicks)

    events

let calibrazionebuilder( app:_, cont:LMController) = 

    let nuovodito  = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger,fun _ -> not (cont.AlreadyCalibrating()))
    let fermo      = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
    let fermo2     = new GroundTerm<_,_>(LeapFeatureTypes.Stabile2)
    let stopmodify = new GroundTerm<_,_>(LeapFeatureTypes.Calibrato)

    let calibrating = ((nuovodito |-> setcalibratingfinger_h app cont) |>> (fermo |-> standingTL_h app cont ) |>> (fermo2 |-> standingLR_h app cont ) |>> (stopmodify |-> nomod_h app cont))
    
    calibrating

let movingmousebuilder(app:_, cont:LMController) = 

    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let moving = new GroundTerm<_,_>(LeapFeatureTypes.Moving)

    let movingnet = (nuovodito |-> ditoapparso_h app cont) |>> !*(moving |-> moving_h cont)
     
    movingnet

let fingerbuilder(app:_, cont:LMController) = 
    
    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let fingernet = !*(nuovodito |-> (setcalibratingfinger_h app cont))

    fingernet



