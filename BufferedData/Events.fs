module BufferData.Events

open System.Collections.Generic
open BufferData.IData
open BufferData.Data
open BufferData.TData

exception DataBufferIDExists of string
exception DataBufferNotFound of string

// The f function will raise the T event if needed
type TEvent<'X,'V> (triggerfun : 'V -> bool, ?active: bool, ?name:string)=
    inherit Event<'X>()

    // da vedere se mettere a false
    let mutable activity = match active with 
                                    | None -> true
                                    | Some  h -> h

    let nome = match name with 
                        | None -> ""
                        | Some x -> x
    
    //let mutable counter = 0
    // restituisce la funzione che attiva il trigger 
    member this.CheckFun(value:'V):bool=   //( l:List<'T> when 'T :> BufferedData) =
    (*
        System.Diagnostics.Debug.WriteLine(nome + counter.ToString())
        if (triggerfun value) then 
            counter <- counter + 1
            System.Diagnostics.Debug.WriteLine("buffer ->" )
            System.Diagnostics.Debug.WriteLine ( (triggerfun value).ToString())
     *) 
        triggerfun value
    
    member this.IsActive():bool = 
        activity

    member this.SetActive(v:bool) = 
        activity <- v


///<summary>  // da vedere come inserire i commenti in modo giusto
///Classe contenente il buffer e a cui passare gli eventi su cui effettuare un controllo<br/>
///I tipi: <'T,'W><br/>
///'T uno dei sottotipi di BufferedData<br/>
///'W il tipo di Tdata che contiene il dato da controllare con i predicati 
///</summary>
///<param name="data">l'oggetto buffer su cui controllare gli eventi</param>
type EventBuffer<'T,'W,'U> when 'T :> BufferedData<'W> and 'W :> Data<'U> (data:'T)  =

    let eventlist = new List<TEvent<_,_>>()
    
    member this.addEvent(t:TEvent<_,_>) = eventlist.Add(t)

    member this.AddItem(d:'W,filter:'W -> bool) = 
        data.AddItem(d,filter)

    member this.AddItem(d:'W) =
        data.AddItem(d)  
        eventlist
            |>Seq.filter( fun x -> (x.IsActive() && x.CheckFun(data)))
            |>Seq.iter(fun x -> x.Trigger(data))


type EventsBuffer<'T,'W,'U> when 'T :> BufferedData<'W> and 'W :> Data<'U> (resultargFun:Dictionary<int,'T> -> 'Z when 'Z:>System.EventArgs)  =

    let eventlist = new List<TEvent<_,_>>()
    let datalist = new Dictionary<int,'T>()
    
    member this.AddDataBuffer(id:int, buff:'T) = 
                    if (datalist.ContainsKey(id)) then 
                                                       raise (DataBufferIDExists("ID Già esistente")) 
                                                  else 
                                                       datalist.Add(id,buff)
 
    member this.addEvent(t:TEvent<_,_>) = eventlist.Add(t)

    member this.AddItem(id:int,d:'W,filter:'W -> bool) = 
        
        if not(datalist.ContainsKey(id)) then raise (DataBufferNotFound("il buffer con quell'id non esiste"))
        
        let databuffer = datalist.Item(id)
        databuffer.AddItem(d,filter)
        this.checkevents()

    member this.AddItem(id,int,d:'W) =
        if not(datalist.ContainsKey(id)) then raise (DataBufferNotFound("il buffer con quell'id non esiste"))
        
        let databuffer = datalist.Item(id)
        databuffer.AddItem(d)  
    
        this.checkevents()

    member private this.checkevents() = 
        eventlist
            |>Seq.filter(fun x-> (x.IsActive() && x.CheckFun(datalist)))
            |>Seq.iter(fun x-> x.Trigger(resultargFun datalist))
        