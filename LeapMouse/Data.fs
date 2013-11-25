module LeapMouse.Data

open BufferData.IData


    // tipo per i T1D
    type Td1d(n:float) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData1D with 
                  member  x.D1 =  n
                  member  x.Time = data

    type Td2d(n1:float,n2:float) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData2D with 
                  member  x.D1 =  n1
                  member  x.D2 =  n2
                  member  x.Time = data

    type Td3d(n1:float,n2:float,n3:float) =
        inherit System.EventArgs()
        
        let data = System.DateTime.Now

        interface TData3D with 
                  member  x.D1 =  n1
                  member  x.D2 =  n2
                  member  x.D3 =  n3
                  member  x.Time = data

    type LeapFeatureTypes =
                | Stabile = 0
                | NewHand = 1
                | NewFinger = 2
                | Moving = 3
                | CalibrationFinger = 4
                | Stabile2 = 5