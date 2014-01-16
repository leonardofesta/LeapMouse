module LeapMouse.EvtFunction

open BufferData.TData
open BufferData.IData
open System
let stationary(timespan,toll) = fun buffer ->  let bb = (buffer:Buffered2D<_>)
                                               bb.StationaryPosition(timespan,toll) && bb.PeriodLength() > timespan

let similarPosition(x:TData3D<_>,y:TData3D<_>) = if (((x.D1 - y.D1) <10.0) && ((x.D2 - y.D2) <10.0) && ((x.D3 - y.D3) < 10.0)) then true else false


let clickevt(timespan) = fun buffer -> let bb = (buffer:Buffered3D<_>)
                                       let dati = (bb.cutBuffer(timespan)).GetArrayBuffer()
                                       let x = bb.DifferenceVector(timespan)
                                       let x1,y1,z1 = x.AveragePosition(timespan)
                                       let xdist,ydist,zdist = bb.ComponentDistance(timespan)
                                       
                                       let simpos = similarPosition(dati.[0], dati.[dati.Length-1])
                             //          System.Console.WriteLine("Valori x y z delle differenze  " + x1.ToString() + "  "  + y1.ToString() + "  " + z1.ToString() )
                             //          System.Console.WriteLine("distanza totale " + zdist.ToString())
                                       if (simpos && x1 <3.0 && y1 <3.0 && zdist>50.0 && 
                                           bb.PeriodLength()> timespan && bb.IsContinuous(timespan,40.0) ) 
                                                then 
                                                    true
                                                else 
                                                    false

let leftclickdown(timespan) = fun buffer ->    let bb = (buffer:Buffered3D<_>)
                                               let dati = (bb.cutBuffer(timespan))
                                               let x = bb.DifferenceVector(timespan)
                                               let last = (List.rev (bb.GetListBuffer())).Head
                                               let x1,y1,z1 = x.AveragePosition(timespan)
                                               let _,grad = dati.FittingToLine(timespan)
                                               let xdist,ydist,zdist = bb.ComponentDistance(timespan)
                                               let zdirection = List.filter(fun x -> (x:>TData3D<_>).D3> -0.5) (x.GetListBuffer())
                                               let zover0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 < 0.0) (dati.GetListBuffer())
                                               let zunder0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 < 0.0) (dati.GetListBuffer())
                                               
//                                               System.Console.WriteLine("Valori x y z delle differenze  " + x1.ToString() + "  "  + y1.ToString() + "  " + z1.ToString() )
//                                               System.Console.WriteLine("last x,y,z  "  + last.D1.ToString() + "   " + last.D2.ToString() + "   "  + last.D3.ToString())
//                                               System.Console.WriteLine("distanza totale " + zdist.ToString())
//                                               System.Console.WriteLine("zdirection " + zdirection.ToString() + " zover  " + zover0.ToString())
//                                               System.Console.WriteLine("periodlenght>timespan + contiunous  "+ (bb.PeriodLength()> timespan).ToString() + "  " + bb.IsContinuous(timespan,100.0).ToString() )
//                                               System.Console.WriteLine("direzioni retta" + grad.[2].ToString())
                                               if (zdist>50.0 && grad.[2] < -300.0 && zunder0 && x1+y1<Math.Abs(z1-10.0) &&
                                                   bb.PeriodLength()> timespan && bb.IsContinuous(timespan,100.0)
                                                   ) 
                                                   then 
                                                       true
                                                   else 
                                                        false
(*                                               if ((float zdirection.Length > 0.9* (float (x.GetListBuffer().Length))) && last.D3< -50.0 && x1 <3.0 && y1 <3.0 && zdist>60.0 && 
                                                   bb.PeriodLength()> timespan && bb.IsContinuous(timespan,50.0) ) 
                                                        then 
                                                            true
                                                        else 
                                                            false
*)

let leftclickup(timespan) = fun buffer ->      let bb = (buffer:Buffered3D<_>)
                                               let dati = (bb.cutBuffer(timespan))
                                               let x = bb.DifferenceVector(timespan)
                                               let x1,y1,z1 = x.AveragePosition(timespan)
                                               let xdist,ydist,zdist = bb.ComponentDistance(timespan)
                                               let zdirection = List.forall(fun x -> (x:>TData3D<_>).D3> -0.5)  (x.GetListBuffer())
                                               let zover0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 > 0.0) (dati.GetListBuffer())
                                     //          System.Console.WriteLine("Valori x y z delle differenze  " + x1.ToString() + "  "  + y1.ToString() + "  " + z1.ToString() )
                                     //          System.Console.WriteLine("distanza totale " + zdist.ToString())
                                               if (//zdist>60.0 && zover0 && x1+y1<Math.Abs(z1-10.0) &&
                                                   zover0 &&
                                                   bb.PeriodLength()> timespan

                                                   ) 
                                                   then 
                                                        true
                                                   else 
                                                        false
(*                          
                                               if (zdirection && zover0 && x1 <1.0 && y1 <1.0 && zdist>100.0 && 
                                                   bb.PeriodLength()> timespan && bb.IsContinuous(timespan,100.0) ) 
                                                        then 
                                                            true
                                                        else 
                                                            false
                                                            *)


let rightclickdown() = fun buffer -> let bb = buffer:Buffered1D<_>
                                     if (bb.Count() > 2)
                                        then
                                            let listainversa = List.rev ( bb.GetListBuffer())
                                            let last = listainversa.Head
                                            let prevlast = listainversa.Tail.Head
                                            if (int prevlast.D1 = 1 && int last.D1 = 2 ) 
                                                                         then 
                                                                            true
                                                                         else 
                                                                            false
                                        else
                                            false

let rightclickup() = fun buffer -> let bb = buffer:Buffered1D<_>

                                   if (bb.Count() > 2)
                                        then
                                            let listainversa = List.rev ( bb.GetListBuffer())
                                            let last = listainversa.Head
                                            let prevlast = listainversa.Tail.Head
                                            if (int prevlast.D1 = 2 && int last.D1 = 1 ) 
                                                                         then 
                                                                            true
                                                                         else 
                                                                            false
                                        else
                                            false

