module LeapMouse.Controller

open LeapMouse.FrameApplication
open System.Windows.Forms
open System
open LeapMouse.MouseInteroperator


type Delegate = delegate of unit -> unit


extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
type LMController(app:TrayApplication) = 
    let mutable fingerid = -1
    let mutable left   = -400.0
    let mutable top    = 800.0
    let mutable right  = 400.0
    let mutable bottom = 5.0
    let mutable LeapH  = 800.0
    let mutable LeapW  = 800.0
    let mutable modifyTL = true
    let mutable modifyBR = true

    let mutable mouseposX = 0L
    let mutable mouseposY = 0L

    let mutable lastclick:System.DateTime = System.DateTime.Now.AddMinutes(-1.0)


    let desktopH = System.Windows.Forms.SystemInformation.VirtualScreen.Height
    let desktopW = System.Windows.Forms.SystemInformation.VirtualScreen.Width

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
        app.Invoke(new Delegate(fun () -> System.Windows.Forms.Cursor.Position <- new System.Drawing.Point( mouseX , mouseY ))
                  )   
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

