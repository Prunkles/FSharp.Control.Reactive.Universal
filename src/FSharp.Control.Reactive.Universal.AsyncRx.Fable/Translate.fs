module internal FSharp.Control.Reactive.Universal.AsyncRx.Fable.Translate

// e -- external, FSharp.Control.AsyncRx
// i -- internal, FSharp.Control.Reactive.Universal.AsyncRx

type private AObvE<'a> = FSharp.Control.IAsyncObserver<'a>
type private ADispE = FSharp.Control.IAsyncRxDisposable
type private AObsE<'a> = FSharp.Control.IAsyncObservable<'a>

type private AObvI<'a> = FSharp.Control.Reactive.Universal.IAsyncObserver<'a>
type private ADispI = FSharp.Control.Reactive.Universal.IAsyncDisposable
type private AObsI<'a> = FSharp.Control.Reactive.Universal.IAsyncObservable<'a>


module Obv =
    
    let inline e2i (e: AObvE<_>) : AObvI<_> =
        { new AObvI<_> with
            member _.OnNextAsync(x) = e.OnNextAsync(x)
            member _.OnErrorAsync(err) = e.OnErrorAsync(err)
            member _.OnCompletedAsync() = e.OnCompletedAsync() }

    let inline i2e (i: AObvI<_>) : AObvE<_> =
        { new AObvE<_> with
            member _.OnNextAsync(x) = i.OnNextAsync(x)
            member _.OnErrorAsync(err) = i.OnErrorAsync(err)
            member _.OnCompletedAsync() = i.OnCompletedAsync() }

module Disp =
    
    let inline e2i (e: ADispE) : ADispI =
        { new ADispI with
            member _.DisposeAsync() = e.DisposeAsync() }
    
    let inline i2e (i: ADispI) : ADispE =
        { new ADispE with
            member _.DisposeAsync() = i.DisposeAsync() }
    
module Obs = 

    let inline e2i (e: AObsE<_>) : AObsI<_> =
        { new AObsI<_> with
            member _.SubscribeAsync(iaobv) = async {
                let! eunsib = e.SubscribeAsync(Obv.i2e iaobv)
                return Disp.e2i eunsib
            }
        }
    
    let inline i2e (i: AObsI<_>) : AObsE<_> =
        { new AObsE<_> with
            member _.SubscribeAsync(eaobv) = async {
                let! iunsub = i.SubscribeAsync(Obv.e2i eaobv)
                return Disp.i2e iunsub
            }
        }
