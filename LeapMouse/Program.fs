﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module LeapMouse.Start

open GestIT
open GestIT.FSharp
open GestIT.Leap
open BufferData.IData
open BufferData.Events
open BufferData.TData
open System.Windows.Forms
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
    leap.Controller.SetPolicyFlags(Leap.Controller.PolicyFlag.POLICYBACKGROUNDFRAMES)


    let buff = new Buffered2D()
    let evbuffer = new EventBuffer<_,_>(buff)


    let handlingfun:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                             if (List.length fingerlist = 1) then
                                                                                                 let a = new Td2d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y)
                                                                                                 evbuffer.AddItem(a)

    let StationaryEvent = new TEvent<_,_> (stationary (5000.0,20.0),true,"DitoStazionario")
    let StationaryEvent2 = new TEvent<_,_> (stationary (5500.0,20.0),true,"DitoStazionario2")

    let MovingEvent = new TEvent<_,_> ((fun x -> true), true, "muovendo")
    evbuffer.addEvent(StationaryEvent)
    evbuffer.addEvent(MovingEvent)
    evbuffer.addEvent(StationaryEvent2)

    leap.ActiveFinger.Add(handlingfun)
    sensor.Listen(LeapFeatureTypes.Stabile  , StationaryEvent.Publish |> Event.map(fun x->x :> System.EventArgs))
    sensor.Listen(LeapFeatureTypes.Stabile2 , StationaryEvent2.Publish |> Event.map(fun x-> x :> System.EventArgs))
    sensor.Listen(LeapFeatureTypes.NewFinger, leap.NewFinger |> Event.map(fun x->x :> System.EventArgs))
    sensor.Listen(LeapFeatureTypes.Moving, MovingEvent.Publish |> Event.map(fun x->x :> System.EventArgs)) 
    leap.Connect() |> ignore
    eventi.ToGestureNet(sensor) |> ignore
    
    Application.Run(app)
    leap.Disconnect() |>ignore
    
//    System.Threading.Thread.Sleep(1000)
    0 // return an integer exit code
