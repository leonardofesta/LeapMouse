module LeapMouse.MouseInteroperator

open System.Runtime.InteropServices


[<DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)>]
extern void mouse_event(System.Int64, System.Int64, System.Int64, System.Int64, System.Int64)

let MOUSEEVENTF_LEFTDOWN    = 0x02L
let MOUSEEVENTF_LEFTUP      = 0x04L
let MOUSEEVENTF_RIGHTDOWN   = 0x08L
let MOUSEEVENTF_RIGHTUP     = 0x10L
let MOUSEEVENTF_MIDDLEDOWN  = 0x20L
let MOUSEEVENTF_MIDDLEUP    = 0x40L
let MOUSEEVENTF_MOVE        = 0x01L
    
let MouseMove(xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_MOVE,xDelta,yDelta,0L,0L)

let MouseLeftClick (xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_LEFTDOWN, xDelta, yDelta, 0L, 0L)
    mouse_event(MOUSEEVENTF_LEFTUP, xDelta, yDelta, 0L, 0L)

let MouseRightClickDown (xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_RIGHTDOWN, xDelta, yDelta, 0L, 0L)

let MouseRightClickUp (xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_RIGHTUP, xDelta, yDelta, 0L, 0L)

let MouseLeftClickDown (xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_LEFTDOWN, xDelta, yDelta, 0L, 0L)

let MouseLeftClickUp (xDelta:int64, yDelta:int64) = 
    mouse_event(MOUSEEVENTF_LEFTUP, xDelta, yDelta, 0L, 0L)
    