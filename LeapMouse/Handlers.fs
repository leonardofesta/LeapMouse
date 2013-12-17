module LeapMouse.Handlers

open LeapMouse.FrameApplication
open LeapMouse.Data
open LeapMouse.Controller
open BufferData.TData
open GestIT.Leap


type Delegate = delegate of unit -> unit

let standingTL_h (app:TrayApplication) (contr:LMController) (sender:_, f:LeapFeatureTypes, e:System.EventArgs) =  
       let ee = e:?>Buffered2D<FingerInfo> 
       let element = ee.GetListBuffer().[( ee.GetListBuffer().Length - 1 )]

       ee.Clear()
       contr.setmouseTopLeft(element.D1,element.D2)
       contr.OpenPopupCalibration2()
//       app.Invoke(new Delegate(fun () -> app.PopText("alto a sinistra ok")
//                                         ))
       |>ignore
       System.Console.WriteLine("angolotopleft")

let standingLR_h (app:TrayApplication) (contr:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) =
       let ee = e:?>Buffered2D<FingerInfo>     
       let element = ee.GetListBuffer().[( ee.GetListBuffer().Length - 1 )]

       ee.Clear()
       contr.setmouseBottomRight(element.D1,element.D2)
//       app.Invoke(new Delegate(fun () -> app.PopText("basso destra ok")
//                                         ))
/// TODO VEDERE SE FARE ALTRO POPUP ... 
       |>ignore
       System.Console.WriteLine("angolobottomright")

let ditoapparso_h (app:TrayApplication) (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
//       app.Invoke(new Delegate(fun () -> app.PopText("dito trovato")))
       controller.OpenPopupCalibration1()
       |>ignore
       System.Console.WriteLine("dito trovato")

let nomod_h (app:TrayApplication) (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
       controller.Modify(false)
       System.Console.WriteLine("stop modifiche per calibrazione")

let setcalibratingfinger_h (app:TrayApplication) (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
       let ee = e:?> LeapSensorEventArgs
       controller.SetCalibratingFinger(ee.ActivityFingers.Head.Id)
       System.Console.WriteLine("stiamo settando il dito")
       app.Invoke(new Delegate(fun () -> app.PopText("dito trovato")))
       |>ignore
       System.Console.WriteLine("dito trovato")


let moving_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 

       let ee = (e:?> Buffered2D<FingerInfo>).GetListBuffer()

       if (ee.Length >1) then
           let element = ee.[( ee.Length - 1 )]

           controller.movemouse(element.D1,element.D2)
           |>ignore

let leftclick_h (app:TrayApplication) (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
        System.Console.WriteLine("Leftclickhandler")
        controller.LeftClickmouse()


let leftclickdown_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
        System.Console.WriteLine("LeftClickDown")
        controller.LeftClickDown()


let leftclickup_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
        System.Console.WriteLine("LeftClickUp")
        controller.LeftClickUp()

let rightclickdown_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
        System.Console.WriteLine("RightClickDown")
        controller.RightClickDown()


let rightclickup_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
        System.Console.WriteLine("RightClickUp")
        controller.RightClickUp()