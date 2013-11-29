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
    let lclick      = new GroundTerm<_,_>(LeapFeatureTypes.LClick)
//    let start = new GroundTerm<_,_>(LeapFeatureTypes.NewHand)

//    let events = ((nuovodito |-> setcalibratingfinger_h app cont) |>> (fermo |-> standingTL_h app cont ) |>> (fermo2 |-> standingLR_h app cont ) |>> (stopmodify |-> nomod_h app cont) |>> !*((moving |-> moving_h app cont)|^| (lclick |-> leftclick_h app cont )) )
//    let calibrazione = (fermo2 |-> standingTL_h app cont) |>> (fermo |-> standingLR_h app cont)
    let events = !*((moving |-> moving_h app cont)|^| (lclick |-> leftclick_h app cont )) 

//    nuovodito |-> ditoapparso_h app

    events

let calibrazionebuilder( app:_, cont:LMController) = 

//    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let fermo = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
    let fermo2 = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
//    let moving = new GroundTerm<_,_>(LeapFeatureTypes.Moving)
//    let start = new GroundTerm<_,_>(LeapFeatureTypes.NewHand)

//    let events = ((nuovodito |-> ditoapparso_h app) |>> (fermo |-> standingTL_h app cont ) |>> (fermo |-> standingLR_h app cont ) )// |>> !*(moving |-> moving_h app cont))
    let calibrazione = (fermo |-> standingTL_h app cont) |>> (fermo2 |-> standingLR_h app cont) 
//    let events = ((nuovodito |-> ditoapparso_h app) |>> !*(moving |-> moving_h app cont))

    calibrazione

let movingmousebuilder(app:_, cont:LMController) = 

    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let moving = new GroundTerm<_,_>(LeapFeatureTypes.Moving)

    let movingnet = (nuovodito |-> ditoapparso_h app cont) |>> !*(moving |-> moving_h app cont)

    movingnet

let fingerbuilder(app:_, cont:LMController) = 
    
    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let fingernet = !*(nuovodito |-> (setcalibratingfinger_h app cont))

    fingernet



