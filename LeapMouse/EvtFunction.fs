module LeapMouse.EvtFunction

open BufferData.TData

let periodoeventi(buffer,timespan) =         let bb = ((buffer:Buffered2D<_>).cutBuffer(timespan+1000.0))
                                             if (bb.Count() > 1)
                                                then
                                                    let lista  = bb.GetListBuffer()
                                                    let ultimo = lista.Item(lista.Length-1)
                                                    let primo  = lista.Head
                                                    let intervallo = ultimo.Time.Subtract(primo.Time)
                                                    
                                                    intervallo.TotalMilliseconds > timespan // vero se il confronto è su dati di almeno tot timespan
                                                else 
                                                    false

let stationary(timespan,toll) = fun buffer ->  let bb = (buffer:Buffered2D<_>)
                                               bb.StationaryPosition(timespan,toll) && periodoeventi(bb,timespan)
