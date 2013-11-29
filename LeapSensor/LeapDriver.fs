namespace GestIT.Leap

open Leap

type LeapActivity =
| NewHand of int list
| ActiveHand of int list
| InactiveHand of int list
| NewFinger of int list
| ActiveFinger of int list
| InactiveFinger of int list
| NewTool of int list
| ActiveTool of int list
| InactiveTool of int list
| NewGesture of int list
| ActiveGesture of int list
| InactiveGesture of int list
| NewScreenTapGesture of int list
| ActiveScreenTapGesture of int list
| InactiveScreenTapGesture of int list
| NewKeyTapGesture of int list
| ActiveKeyTapGesture of int list
| InactiveKeyTapGesture of int list
| NewCircleGesture of int list
| ActiveCircleGesture of int list
| InactiveCircleGesture of int list
| NewSwipeGesture of int list
| ActiveSwipeGesture of int list
| InactiveSwipeGesture of int list


type LeapSensorEventArgs(f:Frame, ?activity) =
  inherit System.EventArgs()

  member this.Frame = f
  member this.Activity = activity

  member this.ActivityHands 
    with get() =
      match activity with
      | Some(NewHand l) | Some(ActiveHand l)-> f.Hands |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.ActivityFingers
    with get() =
      match activity with
      | Some(NewFinger l) | Some(ActiveFinger l)-> f.Fingers |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.ActivityTools
    with get() =
      match activity with
      | Some(NewTool l) | Some(ActiveTool l)-> f.Tools |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.ActivityGestures
    with get() =
      match activity with
      | Some(NewGesture l) | Some(ActiveGesture l)-> f.Gestures() |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.ScreenTapGestures
    with get() =
      match activity with
      | Some(NewScreenTapGesture l) | Some(ActiveScreenTapGesture l)-> f.Gestures() |> Seq.filter(fun g -> g.Type=Leap.Gesture.GestureType.TYPESCREENTAP)  |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.KeyTapGestures
    with get() =
      match activity with
      | Some(NewKeyTapGesture l) | Some(ActiveKeyTapGesture l)-> f.Gestures() |> Seq.filter(fun g -> g.Type=Leap.Gesture.GestureType.TYPEKEYTAP)  |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []
  
  member this.CircleGestures
    with get() =
      match activity with
      | Some(NewCircleGesture l) | Some(ActiveCircleGesture l)-> f.Gestures() |> Seq.filter(fun g -> g.Type=Leap.Gesture.GestureType.TYPECIRCLE)  |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []

  member this.SwipeGestures
    with get() =
      match activity with
      | Some(NewSwipeGesture l) | Some(ActiveSwipeGesture l)-> f.Gestures() |> Seq.filter(fun g -> g.Type=Leap.Gesture.GestureType.TYPESWIPE)  |> Seq.filter (fun h -> l |> List.exists(fun el -> h.Id = el)) |> Seq.toList
      | _ -> []


type LeapSensor() =
  inherit Listener()

  let controller = new Controller()

  let mutable activeHands : int Set = Set.empty
  let mutable activeFingers : int Set = Set.empty
  let mutable activeTools : int Set = Set.empty

  let initEvt = new Event<System.EventArgs>()
  let connectEvt = new Event<System.EventArgs>()
  let exitEvt = new Event<System.EventArgs>()
  let frameEvt = new Event<LeapSensorEventArgs>()
  let newHandEvt = new Event<LeapSensorEventArgs>()
  let activeHandEvt = new Event<LeapSensorEventArgs>()
  let inactiveHandEvt = new Event<LeapSensorEventArgs>()
  let newFingerEvt = new Event<LeapSensorEventArgs>()
  let activeFingerEvt = new Event<LeapSensorEventArgs>()
  let inactiveFingerEvt = new Event<LeapSensorEventArgs>()
  let newToolEvt = new Event<LeapSensorEventArgs>()
  let activeToolEvt = new Event<LeapSensorEventArgs>()
  let inactiveToolEvt = new Event<LeapSensorEventArgs>()
  
  let mutable activeGestures : int Set = Set.empty
  let newGestureEvt = new Event<LeapSensorEventArgs>()
  let activeGestureEvt = new Event<LeapSensorEventArgs>()
  let inactiveGestureEvt = new Event<LeapSensorEventArgs>()

  let mutable activeScreenTapGestures : int Set = Set.empty
  let newScreenTapGestureEvt = new Event<LeapSensorEventArgs>()
  let activeScreenTapGestureEvt = new Event<LeapSensorEventArgs>()
  let inactiveScreenTapGestureEvt = new Event<LeapSensorEventArgs>()

  let mutable activeKeyTapGestures : int Set = Set.empty
  let newKeyTapGestureEvt = new Event<LeapSensorEventArgs>()
  let activeKeyTapGestureEvt = new Event<LeapSensorEventArgs>()
  let inactiveKeyTapGestureEvt = new Event<LeapSensorEventArgs>()

  let mutable activeCircleGestures : int Set = Set.empty
  let newCircleGestureEvt = new Event<LeapSensorEventArgs>()
  let activeCircleGestureEvt = new Event<LeapSensorEventArgs>()
  let inactiveCircleGestureEvt = new Event<LeapSensorEventArgs>()

  let mutable activeSwipeGestures : int Set = Set.empty
  let newSwipeGestureEvt = new Event<LeapSensorEventArgs>()
  let activeSwipeGestureEvt = new Event<LeapSensorEventArgs>()
  let inactiveSwipeGestureEvt = new Event<LeapSensorEventArgs>()



  member this.Controller = controller

  member this.Connect    () = controller.AddListener(this)
  member this.Disconnect () = controller.RemoveListener(this)

  member this.Init = initEvt.Publish
  member this.Connected = connectEvt.Publish
  member this.Exit = exitEvt.Publish
  member this.CurrentFrame = frameEvt.Publish

  member this.NewHand = newHandEvt.Publish
  member this.NewFinger = newFingerEvt.Publish
  member this.NewTool = newToolEvt.Publish
  member this.ActiveHand = activeHandEvt.Publish
  member this.ActiveFinger = activeFingerEvt.Publish
  member this.ActiveTool = activeToolEvt.Publish
  member this.InactiveHand = inactiveHandEvt.Publish
  member this.InactiveFinger = inactiveFingerEvt.Publish
  member this.InactiveTool = inactiveToolEvt.Publish

  member this.NewGesture = newGestureEvt.Publish
  member this.ActiveGesture = activeGestureEvt.Publish
  member this.InactiveGesture = inactiveGestureEvt.Publish

  member this.NewScreenTapGesture = newScreenTapGestureEvt.Publish
  member this.ActiveScreenTapGesture = activeScreenTapGestureEvt.Publish
  member this.InactiveScreenTapGesture = inactiveScreenTapGestureEvt.Publish

  member this.NewKeyTapGesture = newKeyTapGestureEvt.Publish
  member this.ActivewKeyTapGesture = activeKeyTapGestureEvt.Publish
  member this.InactivewKeyTapGesture = inactiveKeyTapGestureEvt.Publish

  member this.NewCircleGesture = newCircleGestureEvt.Publish
  member this.ActiveCircleGesture = activeCircleGestureEvt.Publish
  member this.InactiveCircleGesture = inactiveCircleGestureEvt.Publish

  member this.NewSwipeGesture = newSwipeGestureEvt.Publish
  member this.ActiveSwipeGesture = activeSwipeGestureEvt.Publish
  member this.InactiveSwipeGesture = inactiveSwipeGestureEvt.Publish


  override this.Dispose () =
    this.Disconnect() |> ignore
    base.Dispose()
    controller.Dispose()

  override this.OnInit c = initEvt.Trigger(new System.EventArgs())
  override this.OnConnect c = connectEvt.Trigger(new System.EventArgs())
  override this.OnExit c = exitEvt.Trigger(new System.EventArgs())

  override this.OnFrame c =
    let frame = c.Frame()
    frameEvt.Trigger(new LeapSensorEventArgs(frame))
    let processHands () =
      let hs = frame.Hands |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nh = hs - activeHands
      let ah = hs |> Set.intersect activeHands
      let dh = activeHands - hs
      if not nh.IsEmpty then newHandEvt.Trigger(new LeapSensorEventArgs(frame, NewHand (nh |> Set.toList)))
      if not ah.IsEmpty then activeHandEvt.Trigger(new LeapSensorEventArgs(frame, ActiveHand (ah |> Set.toList)))
      if not dh.IsEmpty then inactiveHandEvt.Trigger(new LeapSensorEventArgs(frame, InactiveHand (dh |> Set.toList)))
      activeHands <- hs

    let processFingers () =
      let fs = frame.Fingers |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nf = fs - activeFingers
      let af = fs |> Set.intersect activeFingers
      let df = activeFingers - fs
      if not nf.IsEmpty then newFingerEvt.Trigger(new LeapSensorEventArgs(frame, NewFinger (nf |> Set.toList)))
      if not af.IsEmpty then activeFingerEvt.Trigger(new LeapSensorEventArgs(frame, ActiveFinger (af |> Set.toList)))
      if not df.IsEmpty then inactiveFingerEvt.Trigger(new LeapSensorEventArgs(frame, InactiveFinger (df |> Set.toList)))
      activeFingers <- fs

    let processTools () =
      let ts = frame.Tools |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeTools
      let at = ts |> Set.intersect activeTools
      let dt = activeTools - ts
      if not nt.IsEmpty then newToolEvt.Trigger(new LeapSensorEventArgs(frame, NewTool (nt |> Set.toList)))
      if not at.IsEmpty then activeToolEvt.Trigger(new LeapSensorEventArgs(frame, ActiveTool (at |> Set.toList)))
      if not dt.IsEmpty then inactiveToolEvt.Trigger(new LeapSensorEventArgs(frame, InactiveTool (dt |> Set.toList)))
      activeTools <- ts

    let processGestures () =
      let ts = frame.Gestures() |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeGestures
      let at = ts |> Set.intersect activeGestures
      let dt = activeGestures - ts
      if not nt.IsEmpty then newGestureEvt.Trigger(new LeapSensorEventArgs(frame, NewGesture (nt |> Set.toList)))
      if not at.IsEmpty then activeGestureEvt.Trigger(new LeapSensorEventArgs(frame, ActiveGesture (at |> Set.toList)))
      if not dt.IsEmpty then inactiveGestureEvt.Trigger(new LeapSensorEventArgs(frame, InactiveGesture (dt |> Set.toList)))
      activeGestures <- ts

    let processScreenTaps () =
      let ts = frame.Gestures() |> Seq.filter(fun f -> f.Type=Gesture.GestureType.TYPESCREENTAP) |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeScreenTapGestures
      let at = ts |> Set.intersect activeScreenTapGestures
      let dt = activeScreenTapGestures - ts
      if not nt.IsEmpty then newScreenTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, NewScreenTapGesture (nt |> Set.toList)))
      if not at.IsEmpty then activeScreenTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, ActiveScreenTapGesture (at |> Set.toList)))
      if not dt.IsEmpty then inactiveScreenTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, InactiveScreenTapGesture (dt |> Set.toList)))
      activeScreenTapGestures <- ts

    let processKeyTaps () =
      let ts = frame.Gestures() |> Seq.filter(fun f -> f.Type=Gesture.GestureType.TYPEKEYTAP) |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeKeyTapGestures
      let at = ts |> Set.intersect activeKeyTapGestures
      let dt = activeKeyTapGestures - ts
      if not nt.IsEmpty then newKeyTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, NewKeyTapGesture (nt |> Set.toList)))
      if not at.IsEmpty then activeKeyTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, ActiveKeyTapGesture (at |> Set.toList)))
      if not dt.IsEmpty then inactiveKeyTapGestureEvt.Trigger(new LeapSensorEventArgs(frame, InactiveKeyTapGesture (dt |> Set.toList)))
      activeKeyTapGestures <- ts

    let processCircle () =
      let ts = frame.Gestures() |> Seq.filter(fun f -> f.Type=Gesture.GestureType.TYPECIRCLE) |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeCircleGestures
      let at = ts |> Set.intersect activeCircleGestures
      let dt = activeCircleGestures - ts
      if not nt.IsEmpty then newCircleGestureEvt.Trigger(new LeapSensorEventArgs(frame, NewCircleGesture (nt |> Set.toList)))
      if not at.IsEmpty then activeCircleGestureEvt.Trigger(new LeapSensorEventArgs(frame, ActiveCircleGesture (at |> Set.toList)))
      if not dt.IsEmpty then inactiveCircleGestureEvt.Trigger(new LeapSensorEventArgs(frame, InactiveCircleGesture (dt |> Set.toList)))
      activeCircleGestures <- ts

    let processSwipe () =
      let ts = frame.Gestures() |> Seq.filter(fun f -> f.Type=Gesture.GestureType.TYPESWIPE) |> Seq.map (fun h -> h.Id) |> Set.ofSeq
      let nt = ts - activeSwipeGestures
      let at = ts |> Set.intersect activeSwipeGestures
      let dt = activeSwipeGestures - ts
      if not nt.IsEmpty then newSwipeGestureEvt.Trigger(new LeapSensorEventArgs(frame, NewSwipeGesture (nt |> Set.toList)))
      if not at.IsEmpty then activeSwipeGestureEvt.Trigger(new LeapSensorEventArgs(frame, ActiveSwipeGesture (at |> Set.toList)))
      if not dt.IsEmpty then inactiveSwipeGestureEvt.Trigger(new LeapSensorEventArgs(frame, InactiveSwipeGesture (dt |> Set.toList)))
      activeSwipeGestures <- ts



    processHands()
    processFingers()
    processTools()
    processGestures()
    processScreenTaps()
    processKeyTaps()
    processCircle()
    processSwipe()