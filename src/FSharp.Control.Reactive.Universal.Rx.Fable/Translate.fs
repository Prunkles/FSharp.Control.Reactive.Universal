module internal FSharp.Control.Reactive.Universal.Rx.Fable.Translate

type private AObvE<'a> = FSharp.Control.IAsyncObserver<'a>
type private ADispE = FSharp.Control.IAsyncRxDisposable
type private AObsE<'a> = FSharp.Control.IAsyncObservable<'a>

type private ObvI<'a> = FSharp.Control.Reactive.Universal.IObserver<'a>
type private DispI = System.IDisposable
type private ObsI<'a> = FSharp.Control.Reactive.Universal.IObservable<'a>

module Obv =
    open FSharp.Control
    
    let inline e2i (ae: AObvE<_>) : ObvI<_> =
        let e = ae.ToObserver()
        { new ObvI<_> with
            member _.OnNext(x) = e.OnNext(x)
            member _.OnError(err) = e.OnError(err)
            member _.OnCompleted() = e.OnCompleted() }
    
    let inline i2e (i: ObvI<_>) : AObvE<_> =
        let ai = i.ToAsyncObserver()
        { new AObvE<_> with
            member _.OnNextAsync(x) = ai.OnNextAsync(x)
            member _.OnErrorAsync(err) = ai.OnErrorAsync(err)
            member _.OnCompletedAsync() = ai.OnCompletedAsync() }

module Disp =
    open FSharp.Control
    
    let inline e2i (ae: ADispE) : DispI =
        let e = ae.ToDisposable()
        { new DispI with
            member _.Dispose() = e.Dispose() }
    
    let inline i2e (i: DispI) : ADispE =
        let ai = i.ToAsyncDisposable()
        { new ADispE with
            member _.DisposeAsync() = ai.DisposeAsync() }
    
module Obs =
    open FSharp.Control

    let inline e2i (ae: AObsE<_>) : ObsI<_> =
        let e = AsyncRx.toObservable ae
        { new ObsI<_> with
            member _.Subscribe(iobv) =
                let eunsib = e.Subscribe(iobv)
                eunsib
        }
    
    let inline i2e (i: ObsI<_>) : AObsE<_> =
        let ai = i.ToAsyncObservable()
        { new AObsE<_> with
            member _.SubscribeAsync(eaobv) = async {
                let! iunsub = ai.SubscribeAsync(eaobv)
                return iunsub
            }
        }
