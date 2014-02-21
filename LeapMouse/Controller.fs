module LeapMouse.Controller

open System.Windows.Forms
open System
open LeapMouse.MouseInteroperator
open LeapMouse.Data
open GestIT.FSharp
open GestIT
open LeapMouse.GUI

type Delegate = delegate of unit -> unit

 ///<summary>
 /// Classe di supporto per instanziare le due reti più semplicemente, 
 /// avendo la possibilità di attivarle e spegnere chiamando un semplice metodo
 ///</summary>
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



///<summary>
/// Classe che gestisce si occupa delle chiamate all'interfaccia grafica e fa da controller di tutte le componenti 
///</summary>
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

    
    ///<summary>
    /// Aggiunge l'handler delle reti al sistema
    ///</summary>
    member this.setNets(nets:NetHandler) = 
        netHandler <- nets |> Some 

    ///<summary>
    ///Setta i valori dell'angolo in alto a sinistra  
    ///</summary>
    member this.setmouseTopLeft(xvar:float,yvar:float) = 
        if (modifyTL) then
            left<- xvar
            top <- yvar
            modifyTL <- false

    ///<summary>
    ///Setta i valori dell'angolo in basso a destra
    ///</summary>
    member this.setmouseBottomRight(xvar:float,yvar:float) = 
            if (modifyBR) then
                right  <- xvar
                bottom <- yvar
                LeapH <- Math.Abs(right - left)
                LeapW <- Math.Abs(top - bottom)
                modifyBR<- false
    
    ///<summary>
    ///Setta la modificabilità del "dito usato nella procedura di calibrazione
    ///Gestit continua ad attivare le reti, ed è necessario impedire questo in modo di far partire più procedure in contemporanea
    ///</summary>
    member this.Modify(b:bool) = 
        modifyTL <- b
        modifyBR <- b

    ///<summary>
    ///invoca il movimento del mouse
    ///</summary>
    member this.movemouse(xvar:float,yvar:float) = 
        // fai il movimento del mouse facendo il rapporto e spostandolo
        let mouseX,mouseY = this.mouseposition(xvar,yvar)
        app.Invoke(new Delegate(fun () -> System.Windows.Forms.Cursor.Position <- new System.Drawing.Point( mouseX , mouseY )))   
        |>ignore

    ///<summary>    
    /// metodo privato che provvede a fare la giusta proporzione tra il desktop e lo spazio del leap 
    ///</summary>
    member private this.mouseposition(leapX:float,leapY:float) =
        let propX = (leapX - left)/LeapW  
        let propY = (top - leapY)/LeapH
        let fmouseX = float desktopW * propX
        let fmouseY = float desktopH * propY
        mouseposX <- int64 fmouseX
        mouseposY <- int64 fmouseY 
        (int fmouseX),(int fmouseY)

    
    ///<summary>
    /// setta l'id del dito che sarà usato per la fase di calibrazione
    ///</summary>
    member this.SetCalibratingFinger(id:int) = 
        if (fingerid = -1 ) then fingerid <- id
        System.Console.WriteLine("dito settato a " + fingerid.ToString())

    ///<summary>
    /// risetta a -1 l'id del dito, che rappresenta nessun dito settato
    ///</summary>
    member this.ClearCalibratingFinger() =
        fingerid <- -1

    ///<summary>
    /// controlla se c'è già una rete in calibrazione
    ///</summary>
    member this.AlreadyCalibrating() =
        if (fingerid = -1) then false
                           else true

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

    ///<summary>
    /// Chiama il delegate che apre il primo popup durante la calibrazione
    ///</summary>
    member this.OpenPopupCalibration1() = 
        if not(popupthread.IsAlive) then popupthread.Start()
                                    else popup.Invoke(new Delegate(fun () -> popup.setText("Positizonati nell'angolo in alto a sinistra del tuo desktop virtuale e attendi 4 secondi")
                                                                             popup.setButtonText("Annulla")
                                                                             popup.Show())) |>ignore
                                             
        |>ignore

    ///<summary>
    /// Chiama il delegate che apre il secondo popup durante la calibrazione
    ///</summary>
    member this.OpenPopupCalibration2() = 
        popup.Invoke(new Delegate(fun () -> popup.Hide()))
        |> ignore
        System.Threading.Thread.Sleep(200)
        popup.Invoke(new Delegate( fun () -> popup.setText("Positizonati nell'angolo in basso a destra del tuo desktop virtuale e attendi 4 secondi")
                                             popup.Show()))
        |>ignore

    ///<summary>
    /// Chiama il delegate che apre il primo popup che conferma la avvenuta calibrazione
    ///</summary>
    member this.ClosePopupCalibration3() = 
        popup.Invoke(new Delegate(fun () -> popup.setText("Settaggio Completato")
                                            popup.setButtonText("Continua")
                                            //popup.Hide()
                                            ))
        |>ignore

    ///<summary>
    /// Aggiorna i margini del desktop virtuale di Leap
    ///</summary>
    member this.SetDesktopCoordinates() = 
        app.Invoke(new Delegate (fun () -> app.setDesktopMargin(int left, int top,
                                                                int right, int bottom)))
        |>ignore



    /// Sezione dei tasti della gui, fa partire e ferma le reti 
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