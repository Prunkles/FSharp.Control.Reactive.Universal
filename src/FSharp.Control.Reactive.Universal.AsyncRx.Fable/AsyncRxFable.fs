module FSharp.Control.Reactive.Universal.AsyncRx

open FSharp.Control.Reactive.Universal.AsyncRx.Fable.Translate


module E = FSharp.Control.AsyncRx

let inline map mapper (source: IAsyncObservable<_>) : IAsyncObservable<_> = E.map mapper (Obs.i2e source) |> Obs.e2i
