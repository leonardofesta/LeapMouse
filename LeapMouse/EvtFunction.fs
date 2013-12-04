module LeapMouse.EvtFunction

open BufferData.TData
open BufferData.IData

let stationary(timespan,toll) = fun buffer ->  let bb = (buffer:Buffered2D<_>)
                                               bb.StationaryPosition(timespan,toll) && bb.PeriodLength() > timespan


let similarPosition(x:TData3D<_>,y:TData3D<_>) = if (((x.D1 - y.D1) <10.0) && ((x.D2 - y.D2) <10.0) && ((x.D3 - y.D3) < 10.0)) then true else false

let clickevt(timespan) = fun buffer -> let bb = (buffer:Buffered3D<_>)
                                       let dati = (bb.cutBuffer(timespan)).GetArrayBuffer()
                                       let x = bb.DifferenceVector(timespan)
                                       let x1,y1,z1 = x.AveragePosition(timespan)
                                       let xdist,ydist,zdist = bb.ComponentDistance(timespan)
                                       
                                       let simpos = similarPosition(dati.[0], dati.[dati.Length-1])
                                       System.Console.WriteLine("Valori x y z delle differenze  " + x1.ToString() + "  "  + y1.ToString() + "  " + z1.ToString() )
                                       System.Console.WriteLine("distanza totale " + zdist.ToString())
                                       if (simpos && x1 <1.0 && y1 <1.0 && zdist>100.0 && 
                                           bb.PeriodLength()> timespan && bb.IsContinuous(timespan,40.0) ) 
                                                then 
                                                    true
                                                else 
                                                    false

