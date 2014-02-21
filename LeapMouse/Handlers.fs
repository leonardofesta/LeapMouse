module LeapMouse.Handlers

open LeapMouse.Data
open LeapMouse.Controller
open BufferData.IData
open BufferData.TData
open GestIT.Leap

//In questo file ci sono gli handler che vengono richiamati dalle reti di petri durante le transizioni in nuove piazze 

type Delegate = delegate of unit -> unit

///<summary>
/// Handler che preso il buffer raccoglie l'ultimo dato e lo passa al controller, 
/// come angolo in alto a sinistra del desktop virtuale 
///</summary>
let standingTL_h (contr:LMController) (sender:_, f:LeapFeatureTypes, e:System.EventArgs) =  
       let ee = e:?>Buffered2D<FingerInfo> 
       let element = ee.GetListBuffer()
                     |> fun x -> (x.Item ( x.Length - 1))  // raccoglie l'ultimo valore della lista
       ee.Clear()
       contr.setmouseTopLeft(element.D1,element.D2)
       contr.OpenPopupCalibration2()
       |>ignore

///<summary>
/// Handler che preso il buffer raccoglie l'ultimo dato e lo passa al controller, 
/// come angolo in basso a destra del desktop virtuale 
///</summary>
let standingLR_h (contr:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) =
       let ee = e:?>Buffered2D<FingerInfo>     
       let element = ee.GetListBuffer()
                     |> fun x -> (x.Item ( x.Length - 1))  // raccoglie l'ultimo valore della lista
       ee.Clear()
       contr.setmouseBottomRight(element.D1,element.D2)
       |>ignore

///<summary>
/// Finisce il processo di modifica e setta le nuove coordinate 
///</summary>
let nomod_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
       controller.Modify(false)
       controller.ClosePopupCalibration3()
       controller.SetDesktopCoordinates()

///<summary>
/// Setta l'id per la calibrazione 
///</summary>
let setcalibratingfinger_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 
       let ee = e:?> LeapSensorEventArgs
       controller.SetCalibratingFinger(ee.ActivityFingers.Head.Id)
       controller.OpenPopupCalibration1()



// Mouse simulating handlers net 

let moving_h (controller:LMController) (sender:_,f:LeapFeatureTypes,e:System.EventArgs) = 

       let ee = (e:?> Buffered2D<FingerInfo>).GetListBuffer()

       if (ee.Length >1) then
           let element = ee.[( ee.Length - 1 )]

           controller.movemouse(element.D1,element.D2)
           |>ignore


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