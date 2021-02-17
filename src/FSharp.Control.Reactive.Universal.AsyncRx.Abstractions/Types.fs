namespace FSharp.Control.Reactive.Universal

type IAsyncObserver<'T> =
    abstract OnNextAsync: value: 'T -> Async<unit>
    abstract OnErrorAsync: error: exn -> Async<unit>
    abstract OnCompletedAsync: unit -> Async<unit>

type IAsyncDisposable =
    abstract DisposeAsync: unit -> Async<unit>

type IAsyncObservable<'T> =
    abstract SubscribeAsync: observer: IAsyncObserver<'T> -> Async<IAsyncDisposable>
