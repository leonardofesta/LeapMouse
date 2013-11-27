module LeapMouse.EvtFunction

open BufferData.TData

let stationary(timespan,toll) = fun buffer ->  let bb = (buffer:Buffered2D<_>)
                                               bb.StationaryPosition(timespan,toll) && bb.PeriodLength() > timespan

