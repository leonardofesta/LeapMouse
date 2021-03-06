﻿// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.
module LeapMouse.Start

open GestIT
open GestIT.FSharp
open GestIT.Leap
open BufferData.IData
open BufferData.Events
open BufferData.Data
open BufferData.TData
open System.Collections.Generic
open System.Windows.Forms
open System.Runtime.InteropServices
open LeapMouse.GUI
open LeapMouse.Data
open LeapMouse.Automa
open LeapMouse.EvtFunction
open LeapMouse.Controller

    
[<EntryPoint>]
let main argv = 
    let sensor = new FusionSensor<LeapFeatureTypes,System.EventArgs>()
    
    let gui = new Form1()
    let popup = new PopupDialog()
    let controller = new LMController(gui,popup)
    let calibrazione = calibrazionebuilder(controller)
    
    let eventi = eventbuilder(controller)
 
    let leap = new LeapSensor()
    leap.Controller.SetPolicyFlags(Leap.Controller.PolicyFlag.POLICYBACKGROUNDFRAMES)

    // Creazione dei nostri buffers

    let rightclickbuffer = new Acc1D<_>()
    let rightclickevbuffer = new EventBuffer<_,_,_> (rightclickbuffer)

    let rightbuffer = new Buffered1D<_>()
    let rightevbuffer = new EventBuffer<_,_,_>(rightbuffer)

    let movementbuffer = new Buffered2D<_>()
    let evbuffer = new EventBuffer<_,_,_>(movementbuffer)

    let clickbuff = new Buffered3D<_>()
    let evclickbuffer = new EventBuffer<_,_,_>(clickbuff)


    // Linking dei tasti della GUI
    gui.CalibrationClickEvt.Add(fun t -> controller.CalibrationClick())
    gui.StartStopClickEvt.Add(fun t -> controller.MovementClick())
    gui.ExitClickEvt.Add(fun t -> gui.Close()
                                  )
    gui.HideClickEvt.Add(fun t -> gui.WindowState <- FormWindowState.Minimized)

    popup.PopupAnnullaEvt.Add(fun t -> controller.CalibrationClick()
                                       popup.Hide()
                                       )

    // toglie il pollice che tende ad apparire a caso ogni tanto dalla lista totale delle dita poichè non si riesce naturalmente a tenere la mano perfettamente chiusa
    let totalfingers:(Leap.Hand -> float) = fun t ->   let hd = t.Direction
                                                       let fingerlist = t.Fingers
                                                       let finger_list = new List<Leap.Finger>()
                                                       for finger in fingerlist 
                                                            do finger_list.Add(finger)
                                                       finger_list
                                                       |> Seq.filter (fun x -> ((float ((x:Leap.Finger).Direction.AngleTo(hd)) * 57.3 )< 30.0 )) 
                                                       |> Seq.length 
                                                       |> float
    
    // Link tra i buffers e i frame del leap sensor
    let rightclickhandler:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                                   let handlist = t.ActivityHands
                                                                   if (List.length handlist = 1) then
                                                                                                    let x = handlist.Head
                                                                                                    new Td1d(totalfingers handlist.Head, new FingerInfo(handlist.Head.Id))
                                                                                                    |>rightevbuffer.AddItem


    let rclickhandler:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                               let handlist = t.ActivityHands
                                                               if (List.length handlist = 1) then
                                                                                                    let x = handlist.Head
                                                                                                    new Ad1d(totalfingers handlist.Head, new FingerInfo(handlist.Head.Id))
                                                                                                    |>rightclickevbuffer.AddItem


    let movementhandlinglinker:(LeapSensorEventArgs -> unit) = fun t -> let fingerlist = t.ActivityFingers
                                                                        if (List.length fingerlist = 1) then
                                                                                                 new Td2d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y, new FingerInfo(fingerlist.Head.Id))
                                                                                                 |> evbuffer.AddItem

    let mouseclicklinker:(LeapSensorEventArgs -> unit ) = fun t -> let fingerlist = t.ActivityFingers
                                                                   if (List.length fingerlist = 1) then 
                                                                                                new Td3d(float fingerlist.Head.StabilizedTipPosition.x, float fingerlist.Head.StabilizedTipPosition.y, float fingerlist.Head.StabilizedTipPosition.z, new FingerInfo(fingerlist.Head.Id))
                                                                                                |> evclickbuffer.AddItem     

    //Creazione TEvents
    let StationaryEvent  = new TEvent<_,_> (stationary (4000.0,40.0),true,"DitoStazionario")
    let StationaryEvent2 = new TEvent<_,_> (stationary (4000.0,40.0),true,"DitoStazionario2")
    let StopModifica     = new TEvent<_,_> ((fun x -> true), true, "stopmodifica")
    let MovingEvent      = new TEvent<_,_> ((fun x -> true), true, "muovendo")
    let RightDown        = new TEvent<_,_> (rightclickdown(),true,"rightclickdown")
    let RightUp          = new TEvent<_,_> (rightclickup()  ,true,"rightclickdown")
    let LeftDown         = new TEvent<_,_> (leftclickdown(300.0),true,"leftclickdown")
    let LeftUp           = new TEvent<_,_> (leftclickup(200.0)  ,true,"leftclickup")

    //Aggiunta ai rispettivi buffers
    evbuffer.addEvent(StationaryEvent)
    evbuffer.addEvent(MovingEvent)
    evbuffer.addEvent(StationaryEvent2)
    evbuffer.addEvent(StopModifica)
    evclickbuffer.addEvent(LeftDown)
    evclickbuffer.addEvent(LeftUp)
    rightevbuffer.addEvent(RightDown)
    rightevbuffer.addEvent(RightUp)

    leap.ActiveHand.Add(rightclickhandler)
    leap.ActiveFinger.Add(movementhandlinglinker)
    leap.ActiveFinger.Add(mouseclicklinker)

    // inscrive le features al sensore legandole a degli eventi, a meno di un upcasting poichè usiamo due tipi diversi ( eventi leap e eventi TEvent)      
    sensor.Listen( LeapFeatureTypes.Stabile   , StationaryEvent.Publish  |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.Stabile2  , StationaryEvent2.Publish |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.NewFinger , leap.NewFinger           |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.Moving    , MovingEvent.Publish      |> Event.map(fun x -> x :> System.EventArgs)) 
    sensor.Listen( LeapFeatureTypes.Calibrato , StopModifica.Publish     |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.RClickDown, RightDown.Publish        |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.RClickUp  , RightUp.Publish          |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.LClickDown, LeftDown.Publish         |> Event.map(fun x -> x :> System.EventArgs))
    sensor.Listen( LeapFeatureTypes.LClickUp  , LeftUp.Publish           |> Event.map(fun x -> x :> System.EventArgs))


    leap.Connect() |> ignore
  
  
    let netshandler = new NetHandler(calibrazione,eventi,sensor)
    controller.setNets(netshandler)
    
    Application.Run(gui)

    leap.Disconnect() |>ignore
    0 // return an integer exit code
