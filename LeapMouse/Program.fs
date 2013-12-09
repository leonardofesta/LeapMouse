// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module LeapMouse.Start

open GestIT
open GestIT.FSharp
open GestIT.Leap
open BufferData.IData
open BufferData.Events
open BufferData.TData
open System.Windows.Forms
open System.Runtime.InteropServices
open LeapMouse.Data
open LeapMouse.FrameApplication
open LeapMouse.Automa
open LeapMouse.EvtFunction
open LeapMouse.Controller


[<EntryPoint>]
let main argv = 
    let sensor = new FusionSensor<LeapFeatureTypes,System.EventArgs>()
    let app = new TrayApplication()
    let controller = new LMController(app)
    // let calibrazione = calibrazionebuilder(app,controller)
    let eventi = eventbuilder(app,controller)
    let leap = new LeapSensor()
    leap.Controller.EnableGesture(Leap.Gesture.GestureType.TYPESCREENTAP)
    leap.Controller.EnableGesture(Leap.Gesture.GestureType.TYPECIRCLE)
    leap.Controller.EnableGesture(Leap.Gesture.GestureType.TYPESWIPE)
    leap.Controller.EnableGesture(Leap.Gesture.GestureType.TYPEKEYTAP)
    leap.Controller.SetPolicyFlags(Leap.Controller.PolicyFlag.POLICYBACKGROUNDFRAMES)

    let rightbuffer = new Buffered1D<_>()
    let rightevbuffer = new EventBuffer<_,_,_>(rightbuffer)

    let buff = new Buffered2D<_>()
    let evbuffer = new EventBuffer<_,_,_>(buff)

    let buffz = new Buffered3D<_>()
    let evbufz = new EventBuffer<_,_,_>(buffz)

    let rightclickhandler:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                                   let handlist = t.ActivityHands
                                                                   if (List.length handlist = 1) then
                                                                                                    new Td1d(float handlist.Head.Fingers.Count, new FingerInfo(handlist.Head.Id))
                                                                                                    |>rightevbuffer.AddItem

    let handlingfun:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                             if (List.length fingerlist = 1) then
                                                                                                 new Td2d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y, new FingerInfo(fingerlist.Head.Id))
                                                                                                 |> evbuffer.AddItem

    let hfunz :(LeapSensorEventArgs -> unit ) = fun t -> let fingerlist = t.ActivityFingers
                                                         if (List.length fingerlist = 1) then 
                                                                                                new Td3d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y, float fingerlist.Head.StabilizedTipPosition.z, new FingerInfo(fingerlist.Head.Id))
                                                                                                |> evbufz.AddItem     

    let StationaryEvent  = new TEvent<_,_> (stationary (4000.0,40.0),true,"DitoStazionario")
    let StationaryEvent2 = new TEvent<_,_> (stationary (3000.0,40.0),true,"DitoStazionario2")
    let StopModifica     = new TEvent<_,_> (( fun x-> true ), true, "stopmodifica")
    let MovingEvent      = new TEvent<_,_> ((fun x -> true), true, "muovendo")
    let Clickevt         = new TEvent<_,_> (clickevt(700.0),true, "clickando")

    let RightClickDown   = new TEvent<_,_> (rightclickdown(),true,"rightclickdown")
    let RightClickUp     = new TEvent<_,_> (rightclickup(),true,"rightclickdown")

    let LeftClickDown    = new TEvent<_,_> (leftclickdown(300.0),true,"leftclickdown")
    let LeftClickUp      = new TEvent<_,_> (leftclickup(200.0),true,"leftclickup")


    evbuffer.addEvent(StationaryEvent)
    evbuffer.addEvent(MovingEvent)
    evbuffer.addEvent(StationaryEvent2)
    evbuffer.addEvent(StopModifica)
//    evbufz.addEvent(Clickevt)
    evbufz.addEvent(LeftClickDown)
    evbufz.addEvent(LeftClickUp)
    rightevbuffer.addEvent(RightClickDown)
    rightevbuffer.addEvent(RightClickUp)

    leap.ActiveHand.Add(rightclickhandler)
    leap.ActiveFinger.Add(handlingfun)
    leap.ActiveFinger.Add(hfunz)

    sensor.Listen( LeapFeatureTypes.Stabile   , StationaryEvent.Publish  |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.Stabile2  , StationaryEvent2.Publish |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.NewFinger , leap.NewFinger           |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.Moving    , MovingEvent.Publish      |> Event.map(fun x -> x :> System.EventArgs)) 
    sensor.Listen( LeapFeatureTypes.Calibrato , StopModifica.Publish     |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.LClick    , Clickevt.Publish         |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.RClickDown, RightClickDown.Publish   |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.RClickUp  , RightClickUp.Publish     |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.LClickDown, LeftClickDown.Publish    |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.LClickUp  , LeftClickUp.Publish      |> Event.map(fun x -> x :> System.EventArgs))


    leap.Connect() |> ignore
    eventi.ToGestureNet(sensor) |> ignore
    
    Application.Run(app)

    leap.Disconnect() |>ignore
    0 // return an integer exit code
//    System.Threading.Thread.Sleep(1000)