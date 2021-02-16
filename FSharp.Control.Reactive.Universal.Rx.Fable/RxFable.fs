module FSharp.Control.Reactive.Universal.Rx

open System

type private IAsyncRxObservableE<'a> = FSharp.Control.IAsyncObservable<'a>
module AsyncRxE = FSharp.Control.AsyncRx

[<AutoOpen>]
module private Helpers =
    open FSharp.Control
    let inline a2s (x: IAsyncRxObservableE<_>) : IObservable<_> = AsyncRxE.toObservable x
    let inline s2a (x: IObservable<_>) : IAsyncRxObservableE<_> = x.ToAsyncObservable()


let inline map mapper source = AsyncRxE.map mapper (s2a source) |> a2s
