﻿module LeapMouse.Controller

open LeapMouse.FrameApplication
open System.Windows.Forms
open System
open LeapMouse.MouseInteroperator
open LeapMouse.Data
open GestIT.FSharp
open GestIT
open LeapMouse.GUI

type Delegate = delegate of unit -> unit

 type NetHandler(calibration:GestureExpr<LeapFeatureTypes,_>, movement:GestureExpr<LeapFeatureTypes,_>, sensor:FusionSensor<LeapFeatureTypes,_>) = 
       
     let mutable calibrationnet = None
     let mutable movementnet = None

     member this.StartCalibration() = 
            calibrationnet <- calibration.ToGestureNet(sensor) |> Some
       
     member this.StartMovement() = 
            movementnet <- movement.ToGestureNet(sensor) |> Some

     member this.StopCalibration() = 
            match calibrationnet with 
                         | Some t -> (t :> System.IDisposable).Dispose()
                         | None   -> ()
            |> ignore

     member this.StopMovement() = 
            match movementnet with 
                         | Some t -> (t :> System.IDisposable).Dispose()
                         | None   -> ()
            |> ignore


extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

type LMController(app:Form1,popup:PopupDialog) = 
    let mutable fingerid = -1
    let mutable left   = -400.0
    let mutable top    = 800.0
    let mutable right  = 400.0
    let mutable bottom = 30.0
    let mutable LeapH  = 800.0
    let mutable LeapW  = 800.0
    let mutable modifyTL = true
    let mutable modifyBR = true

    let mutable mouseposX = 0L
    let mutable mouseposY = 0L

    let mutable lastclick:System.DateTime = System.DateTime.Now.AddMinutes(-1.0)
    let mutable leftclicked:Boolean = false
    let mutable rightcliked:Boolean = false

    let mutable calibrating = false
    let mutable moving = false

    let desktopH = System.Windows.Forms.SystemInformation.VirtualScreen.Height
    let desktopW = System.Windows.Forms.SystemInformation.VirtualScreen.Width

    let mutable netHandler = None

    let popupthread  =  new System.Threading.Thread(fun () -> popup.setText("Positizonati nell'angolo in alto a sinistra del tuo desktop virtuale e attendi 4 secondi")
                                                              Application.Run(popup))

        
    member this.setNets(nets:NetHandler) = 
        netHandler <- nets |> Some 
    
    member this.setmouseTopLeft(xvar:float,yvar:float) = 
        if (modifyTL) then
            left<- xvar
            top <- yvar
            modifyTL <- false
    
    member this.setmouseBottomRight(xvar:float,yvar:float) = 
            if (modifyBR) then
                right  <- xvar
                bottom <- yvar
                LeapH <- Math.Abs(right - left)
                LeapW <- Math.Abs(top - bottom)
                modifyBR<- false
    
    member this.Modify(b:bool) = 
        modifyTL <- b
        modifyBR <- b

    member this.movemouse(xvar:float,yvar:float) = 
        // fai il movimento del mouse facendo il rapporto e spostandolo
        let mouseX,mouseY = this.mouseposition(xvar,yvar)
  //      System.Console.WriteLine(" coordinate Leap x " + xvar.ToString() + " y  " + yvar.ToString()  )
  //      System.Console.WriteLine(" coordinate desktop x = "   + mouseX.ToString() + "   y =  " + mouseY.ToString())
        app.Invoke(new Delegate(fun () -> System.Windows.Forms.Cursor.Position <- new System.Drawing.Point( mouseX , mouseY )))   
        |>ignore

    member private this.mouseposition(leapX:float,leapY:float) =
        let propX = (leapX - left)/LeapW  
        let propY = (top - leapY)/LeapH
        let fmouseX = float desktopW * propX
        let fmouseY = float desktopH * propY
        mouseposX <- int64 fmouseX
        mouseposY <- int64 fmouseY 
        (int fmouseX),(int fmouseY)

    member this.SetCalibratingFinger(id:int) = 
        if (fingerid = -1 ) then fingerid <- id
        System.Console.WriteLine("dito settato a " + fingerid.ToString())

    member this.ClearCalibratingFinger() =
        fingerid <- -1

    member this.AlreadyCalibrating() =
        if (fingerid = -1) then false
                           else true

    member this.LeftClickmouse() = 
        if (System.DateTime.Now.Subtract(lastclick).TotalMilliseconds > 400.0)
            then
            lastclick <- System.DateTime.Now.AddMilliseconds(0.0)
            MouseInteroperator.MouseLeftClick(mouseposX,mouseposY)

    member this.RightClickDown() =
        if not(rightcliked)
            then
                rightcliked <- true
                MouseInteroperator.MouseRightClickDown(mouseposX,mouseposY)

    member this.RightClickUp() = 
        if rightcliked
            then
                rightcliked <- false
                MouseInteroperator.MouseRightClickUp(mouseposX,mouseposY)

    member this.LeftClickDown() =
        if not(leftclicked) 
            then
                leftclicked <- true
                MouseInteroperator.MouseLeftClickDown(mouseposX,mouseposY)

    member this.LeftClickUp() = 
        if leftclicked 
            then
                leftclicked <- false
                MouseInteroperator.MouseLeftClickUp(mouseposX,mouseposY)


// Popup durante la calibrazione
    member this.OpenPopupCalibration1() = 
        if not(popupthread.IsAlive) then popupthread.Start()
                                    else popup.Invoke(new Delegate(fun () -> popup.setText("Positizonati nell'angolo in alto a sinistra del tuo desktop virtuale e attendi 4 secondi")
                                                                             popup.setButtonText("Annulla")
                                                                             popup.Show())) |>ignore
                                             
        |>ignore

    member this.OpenPopupCalibration2() = 
        popup.Invoke(new Delegate(fun () -> popup.Hide()))
        |> ignore
        System.Threading.Thread.Sleep(200)
        popup.Invoke(new Delegate( fun () -> popup.setText("Positizonati nell'angolo in basso a destra del tuo desktop virtuale e attendi 4 secondi")
                                             popup.Show()))
        |>ignore
        
    member this.ClosePopupCalibration3() = 
        popup.Invoke(new Delegate(fun () -> popup.setText("Settaggio Completato")
                                            popup.setButtonText("Continua")
                                            //popup.Hide()
                                            ))
        |>ignore

    member this.SetDesktopCoordinates() = 
        app.Invoke(new Delegate (fun () -> app.setDesktopMargin(int left, int top,
                                                                int right, int bottom)))
        |>ignore



/// Parte per i tasti della gui, fa partire e ferma le reti 
/// TODO vedi se il metodod modify è ancora necessario o fuffa
    member this.StartCalibrationNet() = 
        match netHandler with
            | None   -> ()
            | Some s -> s.StartCalibration()
        calibrating <- true

    member this.StopCalibrationNet() = 
        match netHandler with
            | None   -> ()
            | Some s -> s.StopCalibration()
        calibrating <- false
        this.ClearCalibratingFinger()

    member this.StartMovementNet() = 
        match netHandler with
            | None   -> ()
            | Some s -> s.StartMovement()
        moving <- true
        app.changeStartStopButton(Form1.BUTTONSTOP)

    member this.StopMovementNet() = 
        match netHandler with
            | None   -> ()
            | Some s -> s.StopMovement()
        moving <- false
        app.changeStartStopButton(Form1.BUTTONSTART)

    member this.CalibrationClick() = 
        if calibrating then this.StopCalibrationNet()
                       else this.Modify(true)
                            this.StartCalibrationNet()

    member this.MovementClick() = 
        if moving then this.StopMovementNet()
                  else this.StartMovementNet()