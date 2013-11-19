module LeapMouse.Handlers

open LeapMouse.FrameApplication
open LeapMouse.Data
open BufferData.TData

type Delegate = delegate of unit -> unit


let standingTL_h (app:TrayApplication) (sender:_, f:LeapFeatureTypes, e:_) =  
       let ee = e:?>Buffered2D 
       app.Invoke(new Delegate(fun () -> app.PopText("dito trovato")
                                         ee.Clear()
                                         System.Console.WriteLine("maledetto sender" + sender.GetType().ToString()) 
                                         System.Console.WriteLine("maledetto event " + e.GetType().ToString() )))
       |>ignore
       System.Console.WriteLine("angolotopleft")

let standingLR_h (app:TrayApplication) (sender:_,f:LeapFeatureTypes,e:_) =
       let ee = e:?>Buffered2D   
       app.Invoke(new Delegate(fun () -> app.PopText("dito trovato")
                                         ee.Clear()
                                         System.Console.WriteLine("maledetto sender" + sender.GetType().ToString()) 
                                         System.Console.WriteLine("maledetto event " + e.GetType().ToString() )))
 
       |>ignore
       System.Console.WriteLine("angolobottomright")

let ditoapparso_h (app:TrayApplication) (sender:_,f:LeapFeatureTypes,e:_) = 
       app.Invoke(new Delegate(fun () -> app.PopText("dito trovato")
                                         System.Console.WriteLine("maledetto sender" + sender.GetType().ToString()) 
                                         System.Console.WriteLine("maledetto event " + e.GetType().ToString() )))
       |>ignore

       //("dito trovato")
       System.Console.WriteLine("ditotrovato")