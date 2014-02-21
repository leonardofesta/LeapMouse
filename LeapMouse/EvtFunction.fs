module LeapMouse.EvtFunction

open BufferData.TData
open BufferData.IData
open BufferData.Data

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
                                       if (simpos && x1 <3.0 && y1 <3.0 && zdist>50.0 && 
                                           bb.PeriodLength()> timespan && bb.IsContinuous(timespan,40.0) ) 
                                                then 
                                                    true
                                                else 
                                                    false

let leftclickdown(timespan) = fun buffer ->    let bb = (buffer:Buffered3D<_>)
                                               if (bb.Count() <2) 
                                                then false 
                                                else
                                                   let dati = (bb.cutBuffer(timespan))
                                                   let x = bb.DifferenceVector(timespan)
                                                   let last = bb.GetListBuffer() 
                                                              |> fun x -> (x.Item ( x.Length - 1))
                                                   let x1,y1,z1 = x.AveragePosition(timespan)
                                                   let _,grad = dati.FittingToLine(timespan)
                                                   let _,_,zdist = bb.ComponentDistance(timespan)
                                                   let zunder0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 < 0.0) (dati.GetListBuffer())
                                               
                                                   if (zdist>50.0 && grad.[2] < -300.0 && zunder0 && x1+y1<Math.Abs(z1-10.0) &&
                                                       bb.PeriodLength()> timespan && bb.IsContinuous(timespan,100.0)) 
                                                       then 
                                                           true
                                                       else 
                                                            false

let leftclickup(timespan) = fun buffer ->      let bb = (buffer:Buffered3D<_>)
                                               let dati = (bb.cutBuffer(timespan))
                                               let x = bb.DifferenceVector(timespan)
                                               let zover0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 > 0.0) (dati.GetListBuffer())
                                               if (zover0 && bb.PeriodLength()> timespan) 
                                                   then 
                                                        true
                                                   else 
                                                        false


let rightclickdown() = fun buffer -> let bb = buffer:Buffered1D<_>
                                     if (bb.Count() > 2)
                                        then
                                            let lista = bb.GetListBuffer()
                                            let last = lista.Item ((lista.Length) - 1)
                                            let prevlast = lista.Item ((lista.Length) - 2)
                                            if (int prevlast.D1 = 1 && int last.D1 = 2 ) 
                                                                         then 
                                                                            true
                                                                         else 
                                                                            false
                                        else false

let rightclickup() = fun buffer -> let bb = buffer:Buffered1D<_>

                                   if (bb.Count() > 2)
                                        then
                                            let lista = bb.GetListBuffer()
                                            let last = lista.Item ((lista.Length) - 1)
                                            let prevlast = lista.Item ((lista.Length) - 2)
                                            if (int prevlast.D1 = 2 && int last.D1 = 1 ) 
                                                                         then 
                                                                            true
                                                                         else 
                                                                            false
                                        else false

let rightdown() = fun x -> let bb = (x:Acc1D<_>):>NumericData<Data1D<_>,_>
                           if (bb.Count >2) 
                              then if (int bb.Last.D1  = 2 && int bb.SecondLast.D1 = 1)
                                        then true 
                                        else false
                              else false 
                                            
let rightup() = fun x ->   let bb = (x:Acc1D<_>):>NumericData<Data1D<_>,_>
                           if (bb.Count >2) 
                              then if (int bb.Last.D1  = 1 && int bb.SecondLast.D1 = 2)
                                        then true 
                                        else false
                              else false 
                        