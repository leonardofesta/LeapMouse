module LeapMouse.EvtFunction

open BufferData.TData
open BufferData.IData
open BufferData.Data
open System

///<summary>
/// funzione che verifica la stazionarietà di un punto rispetto ad una certa tolleranza
///</summary>
let stationary(timespan,toll) = fun buffer ->  let bb = (buffer:Buffered2D<_>)
                                               bb.StationaryPosition(timespan,toll) && bb.PeriodLength() > timespan


///<summary>
/// Funzione che rappresenta il leftclick down, il parametro temporale è servito per calibrarla 
///</summary>
let leftclickdown(timespan) = fun buffer ->    let bb = (buffer:Buffered3D<_>)
                                               if (bb.Count() <2) 
                                                then false 
                                                else
                                                   let dati = (bb.cutBuffer(timespan)) // in dati finisce l'ultimo periodo di tempo
                                                   let x = bb.DifferenceVector(timespan) //il vettore calcola le differenze dei valori
                                                   let last = bb.GetListBuffer() 
                                                              |> fun x -> (x.Item ( x.Length - 1))  // prende l'ultimo della lista
                                                   let x1,y1,z1 = x.AveragePosition(timespan)  
                                                   let _,grad = dati.FittingToLine(timespan)  // prendiamo il coefficiente angolare della retta interpolata
                                                   let _,_,zdist = bb.ComponentDistance(timespan)  //distanza percorsa nell'asse X
                                                   let zunder0 =  List.forall ( fun x -> (x:>TData3D<_>).D3 < 0.0) (dati.GetListBuffer())  // controlla che abbiamo superato il valore soglia che è lo 0 su Z
                                               
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
                        