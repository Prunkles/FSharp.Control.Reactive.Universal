module FSharp.Control.Reactive.Universal.Rx

open FSharp.Control.Reactive

module E = Observable

let inline map f source = E.map f source
