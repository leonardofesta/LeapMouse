module LeapMouse.Controller

open LeapMouse.FrameApplication
open System.Windows.Forms
open System

type Delegate = delegate of unit -> unit

type LMController(app:TrayApplication) = 
    let mutable fingerid = -1
    let mutable left   = -300.0
    let mutable top    = 601.0
    let mutable right  = 300.0
    let mutable bottom = 1.0
    let mutable LeapH  = 599.0
    let mutable LeapW  = 600.0

    let desktopH = System.Windows.Forms.SystemInformation.VirtualScreen.Height
    let desktopW = System.Windows.Forms.SystemInformation.VirtualScreen.Width

    member this.setmouseTopLeft(xvar:float,yvar:float) = 
        if (left = 0.0) then
            left<- xvar
            top <- yvar
    
    member this.setmouseBottomRight(xvar:float,yvar:float) = 
        if (right = 0.0) then
            right  <- xvar
            bottom <- yvar
            LeapH <- Math.Abs(right - left)
            LeapW <- Math.Abs(top - bottom)

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
        (int fmouseX),(int fmouseY)

    member this.SetCalibratingFinger(id:int) = 
        if not(fingerid = -1 ) then fingerid <- id

    member this.ClearCalibratingFinger() =
        fingerid <- -1