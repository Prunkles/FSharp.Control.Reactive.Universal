module FSharp.Control.Reactive.Universal.Rx

open FSharp.Control.Reactive.Universal.Rx.Fable.Translate

module E = FSharp.Control.AsyncRx

let inline map f (source: IObservable<_>) : IObservable<_> = E.map f (Obs.i2e source) |> Obs.e2i
