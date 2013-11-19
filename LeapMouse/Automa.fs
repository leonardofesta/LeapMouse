module LeapMouse.Automa

open GestIT
open GestIT.FSharp
open LeapMouse.Data
open LeapMouse.Handlers


let eventbuilder( app:_) = 
    
    let nuovodito = new GroundTerm<_,_>(LeapFeatureTypes.NewFinger)
    let fermo = new GroundTerm<_,_>(LeapFeatureTypes.Stabile)
//    let start = new GroundTerm<_,_>(LeapFeatureTypes.NewHand)

    let events = ((nuovodito |-> ditoapparso_h app) |>> (fermo |-> standingTL_h app ) |>> (fermo |-> standingLR_h app ))

    events