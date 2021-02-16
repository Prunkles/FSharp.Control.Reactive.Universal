module FSharp.Control.Reactive.Universal.AsyncRx

open FSharp.Control.Reactive.Universal.AsyncRx
open FSharp.Control.Reactive.Universal.AsyncRx.Fable.Translate


module E = FSharp.Control.AsyncRx

let inline map mapper (source: IAsyncObservable<_>) : IAsyncObservable<_> =
    E.map mapper (AObs.i2e source) |> AObs.e2i
