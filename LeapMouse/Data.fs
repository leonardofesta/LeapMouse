module LeapMouse.Data

open BufferData.IData

    type FingerInfo(id:int) =
        member this.ID = id 

    // tipo per i T1D
    type Td1d(n:float,info:FingerInfo) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData1D<FingerInfo> with 
                  member  x.D1 =  n
                  member  x.Time = data
                  member  x.Info = info

    type Td2d(n1:float,n2:float,info:FingerInfo) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData2D<FingerInfo> with 
                  member  x.D1 =  n1
                  member  x.D2 =  n2
                  member  x.Info = info
                  member  x.Time = data

    type Td3d(n1:float,n2:float,n3:float,info:FingerInfo) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData3D<FingerInfo> with 
                  member  x.D1 =  n1
                  member  x.D2 =  n2
                  member  x.D3 =  n3
                  member  x.Time = data
                  member  x.Info = info

    type LeapFeatureTypes =
                | Stabile = 0
                | NewHand = 1
                | NewFinger = 2
                | Moving = 3
                | CalibrationFinger = 4
                | Stabile2 = 5
                | Calibrato = 6
                | LClick = 7
                | Rotate = 8
                | ZoomOut = 9
                | ZoomIn = 10