module LeapMouse.Data


/// Modulo con le interfacce per i Dati istanziati
 
open BufferData.IData

    [<AllowNullLiteralAttribute>]
    type FingerInfo(id:int) =
        member this.ID = id 
    
    type Ad1d(n:float,info:FingerInfo) = 
        inherit System.EventArgs()
        interface Data1D<FingerInfo> with 
                    member x.D1 = n
                    member x.Info = info

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
                | Stabile = 1
                | NewFinger = 2
                | Moving = 3
                | Stabile2 = 4
                | Calibrato = 5
                | RClickDown = 6
                | RClickUp = 7
                | LClickDown = 8
                | LClickUp = 9
 

