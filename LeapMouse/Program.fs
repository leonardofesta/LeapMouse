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
open LeapMouse.Data
open LeapMouse.FrameApplication
open LeapMouse.Automa
open LeapMouse.EvtFunction



[<EntryPoint>]
let main argv = 

    let sensor = new FusionSensor<LeapFeatureTypes,System.EventArgs>()
    let app = new TrayApplication()
    let eventi = eventbuilder(app)

    let leap = new LeapSensor()
    leap.Controller.SetPolicyFlags(Leap.Controller.PolicyFlag.POLICYBACKGROUNDFRAMES)

    let buff = new Buffered2D()

    let evbuffer = new EventBuffer<_,_>(buff)

    let handlingfun:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                             if (List.length fingerlist = 1) then
                                                                                                 let a = new Td2d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y)
                                                                                                 evbuffer.AddItem(a)

    let StationaryEvent = new TEvent<_,_> (stationary (5000.0,50.0),true,"DitoStazionario")
    evbuffer.addEvent(StationaryEvent)
 //   let NewFingerEvent  = new TEvent<_,_> (,true,"Nuovo Dito")


    leap.ActiveFinger.Add(handlingfun)
    sensor.Listen(LeapFeatureTypes.Stabile  , StationaryEvent.Publish |> Event.map(fun x->x :> System.EventArgs))
    sensor.Listen(LeapFeatureTypes.NewFinger, leap.NewFinger |> Event.map(fun x->x :> System.EventArgs))
    leap.Connect() |> ignore
    eventi.ToGestureNet(sensor) |> ignore

    Application.Run(app)
    
//    printfn "%A" argv
    System.Console.WriteLine("Bla")
//    System.Threading.Thread.Sleep(1000)
    0 // return an integer exit code
