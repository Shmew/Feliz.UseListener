namespace Feliz.UseListener

open Browser.Types
open Browser.Dom
open Fable.Core
open Fable.Core.JsInterop
open Feliz
open System.ComponentModel

[<EditorBrowsable(EditorBrowsableState.Never)>]
module Impl =
    [<Emit("typeof window !== 'undefined'")>]
    let isWindowDefined () : bool = jsNative

    [<Emit("typeof window.addEventListener === 'function'")>]
    let isWindowListenerFunction () : bool = jsNative

    [<Emit("Object.defineProperty({}, 'passive', {get () { $0() }})")>]
    let definePassive (updater: unit -> unit) : JS.PropertyDescriptor = jsNative

    let allowsPassiveEvents =
        let mutable passive = false

        try
            if isWindowDefined() && isWindowListenerFunction() then
                let options = definePassive(fun () -> passive <- true)

                window.addEventListener("testPassiveEventSupport", ignore, unbox options)
                window.removeEventListener("testPassiveEventSupport", ignore, unbox options)
        with _ -> ()

        passive

    type Document with
        member inline this.addEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.addEventListener(type', listener, unbox {| passive = true |})
            else this.addEventListener(type', listener)

        member inline this.removeEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.removeEventListener(type', listener, unbox {| passive = true |})
            else this.removeEventListener(type', listener)

    type Window with
        member inline this.addEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.addEventListener(type', listener, unbox {| passive = true |})
            else this.addEventListener(type', listener)
        
        member inline this.removeEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.removeEventListener(type', listener, unbox {| passive = true |})
            else this.removeEventListener(type', listener)

    type HTMLElement with
        member inline this.addEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.addEventListener(type', listener, unbox {| passive = true |})
            else this.addEventListener(type', listener)
        
        member inline this.removeEventListenerPassive (type': string, listener: Event -> unit) =
            if allowsPassiveEvents then
                this.removeEventListener(type', listener, unbox {| passive = true |})
            else this.removeEventListener(type', listener)

[<Erase;RequireQualifiedAccess>]
module React =
    open Impl

    [<Erase>]
    type useListener =
        static member inline on (eventType: string, action: #Event -> unit, ?passive: bool) =
            let listener = React.useCallbackRef(fun () ->
                let passive = Option.defaultValue false passive
                let fn = unbox<#Event> >> action

                if passive then
                    document.addEventListenerPassive(eventType, fn)
                else document.addEventListener(eventType, fn)

                React.createDisposable(fun () -> 
                    if passive then
                        document.removeEventListenerPassive(eventType, fn)
                    else document.removeEventListener(eventType, fn)
                )
            )
            
            React.useEffect(listener)

        static member inline onAbort (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("abort", action, ?passive = passive)
        static member inline onAbort (action: UIEvent -> unit, ?passive: bool) = useListener.on("abort", action, ?passive = passive)
        static member inline onAnimationCancel (action: AnimationEvent -> unit, ?passive: bool) = useListener.on("animationcancel", action, ?passive = passive)
        static member inline onAnimationEnd (action: AnimationEvent -> unit, ?passive: bool) = useListener.on("animationend", action, ?passive = passive)
        static member inline onAnimationIteration (action: AnimationEvent -> unit, ?passive: bool) = useListener.on("animationiteration", action, ?passive = passive)
        static member inline onAnimationStart (action: AnimationEvent -> unit, ?passive: bool) = useListener.on("animationstart", action, ?passive = passive)
        static member inline onAuxClick (action: MouseEvent -> unit, ?passive: bool) = useListener.on("onauxclick", action, ?passive = passive)
        static member inline onBlur (action: FocusEvent -> unit, ?passive: bool) = useListener.on("blur", action, ?passive = passive)
        static member inline onCancel (action: Event -> unit, ?passive: bool) = useListener.on("oncancel", action, ?passive = passive)
        static member inline onCanPlay (action: Event -> unit, ?passive: bool) = useListener.on("canplay", action, ?passive = passive)
        static member inline onCanPlayThrough (action: Event -> unit, ?passive: bool) = useListener.on("canplaythrough", action, ?passive = passive)
        static member inline onChange (action: Event -> unit, ?passive: bool) = useListener.on("change", action, ?passive = passive)
        static member inline onClick (action: MouseEvent -> unit, ?passive: bool) = useListener.on("click", action, ?passive = passive)
        static member inline onClose (action: Event -> unit, ?passive: bool) = useListener.on("onclose", action, ?passive = passive)
        static member inline onContextMenu (action: MouseEvent -> unit, ?passive: bool) = useListener.on("contextmenu", action, ?passive = passive)
        static member inline onCopy (action: ClipboardEvent -> unit, ?passive: bool) = useListener.on("copy", action, ?passive = passive)
        static member inline onCueChange (action: Event -> unit, ?passive: bool) = useListener.on("oncuechange", action, ?passive = passive)
        static member inline onCut (action: ClipboardEvent -> unit, ?passive: bool) = useListener.on("cut", action, ?passive = passive)
        static member inline onDblClick (action: MouseEvent -> unit, ?passive: bool) = useListener.on("dblClick", action, ?passive = passive)
        static member inline onDOMContentLoaded (action: MouseEvent -> unit, ?passive: bool) = useListener.on("DOMContentLoaded", action, ?passive = passive)
        static member inline onDrag (action: DragEvent -> unit, ?passive: bool) = useListener.on("drag", action, ?passive = passive)
        static member inline onDragEnd (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragend", action, ?passive = passive)
        static member inline onDragEnter (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragenter", action, ?passive = passive)
        static member inline onDragExit (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragexit", action, ?passive = passive)
        static member inline onDragLeave (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragleave", action, ?passive = passive)
        static member inline onDragOver (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragover", action, ?passive = passive)
        static member inline onDragStart (action: DragEvent -> unit, ?passive: bool) = useListener.on("dragstart", action, ?passive = passive)
        static member inline onDrop (action: DragEvent -> unit, ?passive: bool) = useListener.on("drop", action, ?passive = passive)
        static member inline onDurationChange (action: Event -> unit, ?passive: bool) = useListener.on("durationchange", action, ?passive = passive)
        static member inline onEmptied (action: Event -> unit, ?passive: bool) = useListener.on("emptied", action, ?passive = passive)
        static member inline onEnded (action: Event -> unit, ?passive: bool) = useListener.on("ended", action, ?passive = passive)
        static member inline onError (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("error", action, ?passive = passive)
        static member inline onError (action: UIEvent -> unit, ?passive: bool) = useListener.on("error", action, ?passive = passive)
        static member inline onFocus (action: FocusEvent -> unit, ?passive: bool) = useListener.on("focus", action, ?passive = passive)
        static member inline onFormData (action: Event -> unit, ?passive: bool) = useListener.on("formdata", action, ?passive = passive)
        static member inline onFullscreenChange (action: Event -> unit, ?passive: bool) = useListener.on("fullscreenchange", action, ?passive = passive)
        static member inline onFullscreenError (action: Event -> unit, ?passive: bool) = useListener.on("fullscreenerror", action, ?passive = passive)
        static member inline onGotPointerCapture (action: PointerEvent -> unit, ?passive: bool) = useListener.on("gotPointercapture", action, ?passive = passive)
        static member inline onInput (action: UIEvent -> unit, ?passive: bool) = useListener.on("input", action, ?passive = passive)
        static member inline onInvalid (action: Event -> unit, ?passive: bool) = useListener.on("invalid", action, ?passive = passive)
        static member inline onKeyDown (action: KeyboardEvent -> unit, ?passive: bool) = useListener.on("keydown", action, ?passive = passive)
        static member inline onKeyPress (action: KeyboardEvent -> unit, ?passive: bool) = useListener.on("keypress", action, ?passive = passive)
        static member inline onKeyUp (action: KeyboardEvent -> unit, ?passive: bool) = useListener.on("keyup", action, ?passive = passive)
        static member inline onLoad (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("load", action, ?passive = passive)
        static member inline onLoad (action: UIEvent -> unit, ?passive: bool) = useListener.on("load", action, ?passive = passive)
        static member inline onLoadedData (action: Event -> unit, ?passive: bool) = useListener.on("loadeddata", action, ?passive = passive)
        static member inline onLoadedMetadata (action: Event -> unit, ?passive: bool) = useListener.on("loadedmetadata", action, ?passive = passive)
        static member inline onLoadEnd (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("loadend", action, ?passive = passive)
        static member inline onLoadStart (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("loadstart", action, ?passive = passive)
        static member inline onLostPointerCapture (action: PointerEvent -> unit, ?passive: bool) = useListener.on("lostpointercapture", action, ?passive = passive)
        static member inline onMouseDown (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mousedown", action, ?passive = passive)
        static member inline onMouseEnter (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mouseenter", action, ?passive = passive)
        static member inline onMouseLeave (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mouseleave", action, ?passive = passive)
        static member inline onMouseMove (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mousemove", action, ?passive = passive)
        static member inline onMouseOut (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mouseout", action, ?passive = passive)
        static member inline onMouseOver (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mouseover", action, ?passive = passive)
        static member inline onMouseUp (action: MouseEvent -> unit, ?passive: bool) = useListener.on("mouseup", action, ?passive = passive)
        static member inline onPaste (action: ClipboardEvent -> unit, ?passive: bool) = useListener.on("paste", action, ?passive = passive)
        static member inline onPause (action: Event -> unit, ?passive: bool) = useListener.on("pause", action, ?passive = passive)
        static member inline onPlay (action: Event -> unit, ?passive: bool) = useListener.on("play", action, ?passive = passive)
        static member inline onPlaying (action: Event -> unit, ?passive: bool) = useListener.on("playing", action, ?passive = passive)
        static member inline onPointerCancel (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointercancel", action, ?passive = passive)
        static member inline onPointerDown (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerdown", action, ?passive = passive)
        static member inline onPointerEnter (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerenter", action, ?passive = passive)
        static member inline onPointerLeave (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerleave", action, ?passive = passive)
        static member inline onPointerMove (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointermove", action, ?passive = passive)
        static member inline onPointerOut (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerout", action, ?passive = passive)
        static member inline onPointerOver (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerover", action, ?passive = passive)
        static member inline onPointerUp (action: PointerEvent -> unit, ?passive: bool) = useListener.on("pointerup", action, ?passive = passive)
        static member inline onProgress (action: ProgressEvent -> unit, ?passive: bool) = useListener.on("progress", action, ?passive = passive)
        static member inline onRateChange (action: Event -> unit, ?passive: bool) = useListener.on("ratechange", action, ?passive = passive)
        static member inline onReadyStateChange (action: Event -> unit, ?passive: bool) = useListener.on("readystatechange", action, ?passive = passive)
        static member inline onReset (action: Event -> unit, ?passive: bool) = useListener.on("reset", action, ?passive = passive)
        static member inline onResize (action: Event -> unit, ?passive: bool) = useListener.on("resize", action, ?passive = passive)
        static member inline onScroll (action: Event -> unit, ?passive: bool) = useListener.on("scroll", action, ?passive = passive)
        static member inline onSeeked (action: Event -> unit, ?passive: bool) = useListener.on("seeked", action, ?passive = passive)
        static member inline onSeeking (action: Event -> unit, ?passive: bool) = useListener.on("seeking", action, ?passive = passive)
        static member inline onSelect (action: MouseEvent -> unit, ?passive: bool) = useListener.on("select", action, ?passive = passive)
        static member inline onSelectionChange (action: Event -> unit, ?passive: bool) = useListener.on("selectionchange", action, ?passive = passive)
        static member inline onSelectStart (action: Event -> unit, ?passive: bool) = useListener.on("selectstart", action, ?passive = passive)
        static member inline onStalled (action: Event -> unit, ?passive: bool) = useListener.on("stalled", action, ?passive = passive)
        static member inline onSubmit (action: Event -> unit, ?passive: bool) = useListener.on("submit", action, ?passive = passive)
        static member inline onSuspend (action: Event -> unit, ?passive: bool) = useListener.on("suspend", action, ?passive = passive)
        static member inline onTimeUpdate (action: Event -> unit, ?passive: bool) = useListener.on("timeupdate", action, ?passive = passive)
        static member inline onTouchCancel (action: TouchEvent -> unit, ?passive: bool) = useListener.on("touchcancel", action, ?passive = passive)
        static member inline onTouchEnd (action: TouchEvent -> unit, ?passive: bool) = useListener.on("touchend", action, ?passive = passive)
        static member inline onTouchMove (action: TouchEvent -> unit, ?passive: bool) = useListener.on("touchmove", action, ?passive = passive)
        static member inline onTouchStart (action: TouchEvent -> unit, ?passive: bool) = useListener.on("touchstart", action, ?passive = passive)
        static member inline onTransitionCancel (action: TransitionEvent -> unit, ?passive: bool) = useListener.on("transitioncancel", action, ?passive = passive)
        static member inline onTransitionEnd (action: TransitionEvent -> unit, ?passive: bool) = useListener.on("transitionend", action, ?passive = passive)
        static member inline onTransitionRun (action: TransitionEvent -> unit, ?passive: bool) = useListener.on("transitionrun", action, ?passive = passive)
        static member inline onTransitionStart (action: TransitionEvent -> unit, ?passive: bool) = useListener.on("transitionstart", action, ?passive = passive)
        static member inline onVisibilityChange (action: Event -> unit, ?passive: bool) = useListener.on("visibilitychange", action, ?passive = passive)
        static member inline onVolumeChange (action: Event -> unit, ?passive: bool) = useListener.on("volumechange", action, ?passive = passive)
        static member inline onWaiting (action: Event -> unit, ?passive: bool) = useListener.on("waiting", action, ?passive = passive)
        static member inline onWheel (action: MouseEvent -> unit, ?passive: bool) = useListener.on("wheel", action, ?passive = passive)

        /// Invokes the callback when a click event is not within the given element.
        ///
        /// Uses separate handlers for touch and mouse events.
        ///
        /// This listener is passive by default.
        static member inline onClickAway (elemRef: IRefValue<#HTMLElement option>, callback: MouseEvent -> unit, touchCallback: TouchEvent -> unit, ?disablePassive: bool) =
            let passive = Option.defaultValue false disablePassive |> not

            useListener.onMouseDown((fun ev ->
                match elemRef.current with
                | Some elem when not (elem.contains(unbox ev.target)) ->
                    callback ev
                | _ -> ()
            ), passive = passive)

            useListener.onTouchStart((fun ev ->
                match elemRef.current with
                | Some elem when not (elem.contains(unbox ev.target)) ->
                    touchCallback ev
                | _ -> ()
            ), passive = passive)
        
        /// Invokes the callback when a click event is not within the given element.
        ///
        /// Shares a common callback for both touch and mouse events.
        ///
        /// This listener is passive by default.
        static member inline onClickAway (elemRef: IRefValue<#HTMLElement option>, callback: UIEvent -> unit, ?disablePassive: bool) =
            let passive = Option.defaultValue false disablePassive |> not

            useListener.onMouseDown((fun ev ->
                match elemRef.current with
                | Some elem when not (elem.contains(unbox ev.target)) ->
                    callback ev
                | _ -> ()
            ), passive = passive)

            useListener.onTouchStart((fun ev ->
                match elemRef.current with
                | Some elem when not (elem.contains(unbox ev.target)) ->
                    callback ev
                | _ -> ()
            ), passive = passive)

    [<Erase>]
    type useElementListener =
        static member inline on (elemRef: IRefValue<#HTMLElement option>, eventType: string, action: #Event -> unit, ?passive: bool) =
            let listener = React.useCallbackRef(fun () ->
                let passive = Option.defaultValue false passive
                let fn = unbox<#Event> >> action

                elemRef.current |> Option.iter(fun elem ->
                    if passive then
                        elem.addEventListenerPassive(eventType, fn)
                    else elem.addEventListener(eventType, fn)
                )

                React.createDisposable(fun () -> 
                    elemRef.current |> Option.iter(fun elem ->
                        if passive then
                            elem.removeEventListenerPassive(eventType, fn)
                        else elem.removeEventListener(eventType, fn)
                ))
            )

            React.useEffect(listener)

        static member inline onAbort (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "abort", action, ?passive = passive)
        static member inline onAbort (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "abort", action, ?passive = passive)
        static member inline onAnimationCancel (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "animationcancel", action, ?passive = passive)
        static member inline onAnimationEnd (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "animationend", action, ?passive = passive)
        static member inline onAnimationIteration (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "animationiteration", action, ?passive = passive)
        static member inline onAnimationStart (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "animationstart", action, ?passive = passive)
        static member inline onAuxClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "onauxclick", action, ?passive = passive)
        static member inline onBlur (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "blur", action, ?passive = passive)
        static member inline onCancel (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "oncancel", action, ?passive = passive)
        static member inline onCanPlay (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "canplay", action, ?passive = passive)
        static member inline onCanPlayThrough (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "canplaythrough", action, ?passive = passive)
        static member inline onChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "change", action, ?passive = passive)
        static member inline onClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "click", action, ?passive = passive)
        static member inline onClose (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "onclose", action, ?passive = passive)
        static member inline onContextMenu (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "contextmenu", action, ?passive = passive)
        static member inline onCopy (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "copy", action, ?passive = passive)
        static member inline onCueChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "oncuechange", action, ?passive = passive)
        static member inline onCut (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "cut", action, ?passive = passive)
        static member inline onDblClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dblClick", action, ?passive = passive)
        static member inline onDOMContentLoaded (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "DOMContentLoaded", action, ?passive = passive)
        static member inline onDrag (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "drag", action, ?passive = passive)
        static member inline onDragEnd (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragend", action, ?passive = passive)
        static member inline onDragEnter (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragenter", action, ?passive = passive)
        static member inline onDragExit (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragexit", action, ?passive = passive)
        static member inline onDragLeave (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragleave", action, ?passive = passive)
        static member inline onDragOver (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragover", action, ?passive = passive)
        static member inline onDragStart (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "dragstart", action, ?passive = passive)
        static member inline onDrop (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "drop", action, ?passive = passive)
        static member inline onDurationChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "durationchange", action, ?passive = passive)
        static member inline onEmptied (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "emptied", action, ?passive = passive)
        static member inline onEnded (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "ended", action, ?passive = passive)
        static member inline onError (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "error", action, ?passive = passive)
        static member inline onError (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "error", action, ?passive = passive)
        static member inline onFocus (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "focus", action, ?passive = passive)
        static member inline onFocusIn (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "focusin", action, ?passive = passive)
        static member inline onFocusOut (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "focusout", action, ?passive = passive)
        static member inline onFormData (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "formdata", action, ?passive = passive)
        static member inline onFullscreenChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "fullscreenchange", action, ?passive = passive)
        static member inline onFullscreenError (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "fullscreenerror", action, ?passive = passive)
        static member inline onGotPointerCapture (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "gotPointercapture", action, ?passive = passive)
        static member inline onInput (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "input", action, ?passive = passive)
        static member inline onInvalid (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "invalid", action, ?passive = passive)
        static member inline onKeyDown (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "keydown", action, ?passive = passive)
        static member inline onKeyPress (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "keypress", action, ?passive = passive)
        static member inline onKeyUp (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "keyup", action, ?passive = passive)
        static member inline onLoad (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "load", action, ?passive = passive)
        static member inline onLoad (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "load", action, ?passive = passive)
        static member inline onLoadedData (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "loadeddata", action, ?passive = passive)
        static member inline onLoadedMetadata (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "loadedmetadata", action, ?passive = passive)
        static member inline onLoadEnd (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "loadend", action, ?passive = passive)
        static member inline onLoadStart (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "loadstart", action, ?passive = passive)
        static member inline onLostPointerCapture (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "lostpointercapture", action, ?passive = passive)
        static member inline onMouseDown (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mousedown", action, ?passive = passive)
        static member inline onMouseEnter (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mouseenter", action, ?passive = passive)
        static member inline onMouseLeave (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mouseleave", action, ?passive = passive)
        static member inline onMouseMove (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mousemove", action, ?passive = passive)
        static member inline onMouseOut (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mouseout", action, ?passive = passive)
        static member inline onMouseOver (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mouseover", action, ?passive = passive)
        static member inline onMouseUp (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "mouseup", action, ?passive = passive)
        static member inline onPaste (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "paste", action, ?passive = passive)
        static member inline onPause (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "pause", action, ?passive = passive)
        static member inline onPlay (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "play", action, ?passive = passive)
        static member inline onPlaying (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "playing", action, ?passive = passive)
        static member inline onPointerCancel (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointercancel", action, ?passive = passive)
        static member inline onPointerDown (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerdown", action, ?passive = passive)
        static member inline onPointerEnter (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerenter", action, ?passive = passive)
        static member inline onPointerLeave (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerleave", action, ?passive = passive)
        static member inline onPointerMove (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointermove", action, ?passive = passive)
        static member inline onPointerOut (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerout", action, ?passive = passive)
        static member inline onPointerOver (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerover", action, ?passive = passive)
        static member inline onPointerUp (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "pointerup", action, ?passive = passive)
        static member inline onProgress (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "progress", action, ?passive = passive)
        static member inline onRateChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "ratechange", action, ?passive = passive)
        static member inline onReadyStateChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "readystatechange", action, ?passive = passive)
        static member inline onReset (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "reset", action, ?passive = passive)
        static member inline onResize (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "resize", action, ?passive = passive)
        static member inline onScroll (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "scroll", action, ?passive = passive)
        static member inline onSeeked (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "seeked", action, ?passive = passive)
        static member inline onSeeking (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "seeking", action, ?passive = passive)
        static member inline onSelect (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "select", action, ?passive = passive)
        static member inline onSelectionChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "selectionchange", action, ?passive = passive)
        static member inline onSelectStart (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "selectstart", action, ?passive = passive)
        static member inline onStalled (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "stalled", action, ?passive = passive)
        static member inline onSubmit (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "submit", action, ?passive = passive)
        static member inline onSuspend (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "suspend", action, ?passive = passive)
        static member inline onTimeUpdate (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "timeupdate", action, ?passive = passive)
        static member inline onTouchCancel (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "touchcancel", action, ?passive = passive)
        static member inline onTouchEnd (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "touchend", action, ?passive = passive)
        static member inline onTouchMove (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "touchmove", action, ?passive = passive)
        static member inline onTouchStart (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "touchstart", action, ?passive = passive)
        static member inline onTransitionCancel (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "transitioncancel", action, ?passive = passive)
        static member inline onTransitionEnd (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "transitionend", action, ?passive = passive)
        static member inline onTransitionRun (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "transitionrun", action, ?passive = passive)
        static member inline onTransitionStart (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "transitionstart", action, ?passive = passive)
        static member inline onVisibilityChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "visibilitychange", action, ?passive = passive)
        static member inline onVolumeChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "volumechange", action, ?passive = passive)
        static member inline onWaiting (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit, ?passive: bool) = useElementListener.on(elemRef, "waiting", action, ?passive = passive)
        static member inline onWheel (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit, ?passive: bool) = useElementListener.on(elemRef, "wheel", action, ?passive = passive)

    [<Erase>]
    type useStyle =
        static member inline onCustom (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) (resolver: #HTMLElement option -> bool) =
            let isTrue,setIsTrue = React.useState false
            
            useElementListener.onChange(elemRef, (fun _ ->
                elemRef.current
                |> resolver
                |> setIsTrue
            ), passive = true)

            React.useMemo((fun () -> 
                if isTrue then styles
                else []
            ), [| isTrue :> obj; styles :> obj |])

        static member inline onClick (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) =
            let isClicked,setIsClicked = React.useState false
            
            useElementListener.onClick(elemRef, (fun _ -> setIsClicked (not isClicked)), passive = true)
            
            React.useMemo((fun () -> 
                if isClicked then styles
                else []
            ), [| isClicked :> obj; styles :> obj |])

        static member inline onDisabled< ^elem when ^elem :> HTMLElement and ^elem : (member disabled : bool)> (elemRef: IRefValue< ^elem option>) (styles: IStyleAttribute list) =
            useStyle.onCustom elemRef styles (fun elem ->
                elem
                |> Option.map (fun e -> e?disabled)
                |> Option.defaultValue false
            )

        static member inline onFocus (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) =
            let isFocused,setIsFocused = React.useState false
            
            useElementListener.onFocusIn(elemRef, (fun _ -> setIsFocused true), passive = true)
            useElementListener.onFocusOut(elemRef, (fun _ -> setIsFocused false), passive = true)

            React.useMemo((fun () -> 
                if isFocused then styles
                else []
            ), [| isFocused :> obj; styles :> obj |])

        static member inline onHover (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) =
            let isHovered,setIsHovered = React.useState false
            
            useElementListener.onMouseEnter(elemRef, (fun _ -> setIsHovered true), passive = true)
            useElementListener.onMouseLeave(elemRef, (fun _ -> setIsHovered false), passive = true)

            React.useMemo((fun () -> 
                if isHovered then styles
                else []
            ), [| isHovered :> obj; styles :> obj |])

        static member inline onInvalid< ^elem when ^elem :> HTMLElement and ^elem : (member validity : ValidityState)> (elemRef: IRefValue< ^elem option>) (styles: IStyleAttribute list) =
            useStyle.onCustom elemRef styles (fun elem ->
                elem
                |> Option.map (fun e -> 
                    JS.console.log(e?validity?valid)
                    not e?validity?valid)
                |> Option.defaultValue false
            )

        static member inline onValid< ^elem when ^elem :> HTMLElement and ^elem : (member validity : ValidityState)> (elemRef: IRefValue< ^elem option>) (styles: IStyleAttribute list) =
            useStyle.onCustom elemRef styles (fun elem ->
                elem
                |> Option.map (fun e -> 
                    JS.console.log(e?validity?valid)
                    e?validity?valid)
                |> Option.defaultValue false
            )

    [<Erase>]
    type useWindowListener =
        static member inline on (eventType: string, action: #Event -> unit, ?passive: bool) =
            let listener = React.useCallbackRef(fun () ->
                let passive = Option.defaultValue false passive
                let fn = unbox<#Event> >> action

                if passive then
                    window.addEventListenerPassive(eventType, fn)
                else window.addEventListener(eventType, fn)

                React.createDisposable(fun () ->
                    if passive then
                        window.removeEventListenerPassive(eventType, fn)
                    else window.removeEventListener(eventType, fn)
                )
            )
            
            React.useEffect(listener)
        
        static member inline onAbort (action: ProgressEvent -> unit, ?passive: bool) = useWindowListener.on("abort", action, ?passive = passive)
        static member inline onAbort (action: UIEvent -> unit, ?passive: bool) = useWindowListener.on("abort", action, ?passive = passive)
        static member inline onAnimationCancel (action: AnimationEvent -> unit, ?passive: bool) = useWindowListener.on("animationcancel", action, ?passive = passive)
        static member inline onAnimationEnd (action: AnimationEvent -> unit, ?passive: bool) = useWindowListener.on("animationend", action, ?passive = passive)
        static member inline onAnimationIteration (action: AnimationEvent -> unit, ?passive: bool) = useWindowListener.on("animationiteration", action, ?passive = passive)
        static member inline onAnimationStart (action: AnimationEvent -> unit, ?passive: bool) = useWindowListener.on("animationstart", action, ?passive = passive)
        static member inline onAuxClick (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("onauxclick", action, ?passive = passive)
        static member inline onAfterPrint (action: Event -> unit, ?passive: bool) = useWindowListener.on("afterprint", action, ?passive = passive)
        static member inline onBeforeInstallPrompt (action: Event -> unit, ?passive: bool) = useWindowListener.on("beforeinstallprompt", action, ?passive = passive)
        static member inline onBeforePrint (action: Event -> unit, ?passive: bool) = useWindowListener.on("beforeprint", action, ?passive = passive)
        static member inline onBeforeUnload (action: Event -> unit, ?passive: bool) = useWindowListener.on("beforeunload", action, ?passive = passive)
        static member inline onBlur (action: FocusEvent -> unit, ?passive: bool) = useWindowListener.on("blur", action, ?passive = passive)
        static member inline onChange (action: Event -> unit, ?passive: bool) = useWindowListener.on("change", action, ?passive = passive)
        static member inline onClick (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("click", action, ?passive = passive)
        static member inline onClipboardChange (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("clipboardchange", action, ?passive = passive)
        static member inline onClose (action: Event -> unit, ?passive: bool) = useWindowListener.on("onclose", action, ?passive = passive)
        static member inline onContextMenu (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("contextmenu", action, ?passive = passive)
        static member inline onCopy (action: ClipboardEvent -> unit, ?passive: bool) = useWindowListener.on("copy", action, ?passive = passive)
        static member inline onCut (action: ClipboardEvent -> unit, ?passive: bool) = useWindowListener.on("cut", action, ?passive = passive)
        static member inline onDblClick (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("dblClick", action, ?passive = passive)
        static member inline onDeviceMotion (action: DeviceMotionEvent -> unit, ?passive: bool) = useWindowListener.on("devicemotion", action, ?passive = passive)
        static member inline onDeviceOrientation (action: DeviceOrientationEvent -> unit, ?passive: bool) = useWindowListener.on("deviceorientation", action, ?passive = passive)
        static member inline onDOMContentLoaded (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("DOMContentLoaded", action, ?passive = passive)
        static member inline onDurationChange (action: Event -> unit, ?passive: bool) = useWindowListener.on("durationchange", action, ?passive = passive)
        static member inline onError (action: ProgressEvent -> unit, ?passive: bool) = useWindowListener.on("error", action, ?passive = passive)
        static member inline onError (action: UIEvent -> unit, ?passive: bool) = useWindowListener.on("error", action, ?passive = passive)
        static member inline onFocus (action: FocusEvent -> unit, ?passive: bool) = useWindowListener.on("focus", action, ?passive = passive)
        static member inline onHashChange (action: HashChangeEvent -> unit, ?passive: bool) = useWindowListener.on("hashchange", action, ?passive = passive)
        static member inline onInput (action: UIEvent -> unit, ?passive: bool) = useWindowListener.on("input", action, ?passive = passive)
        static member inline onInvalid (action: Event -> unit, ?passive: bool) = useWindowListener.on("invalid", action, ?passive = passive)
        static member inline onKeyDown (action: KeyboardEvent -> unit, ?passive: bool) = useWindowListener.on("keydown", action, ?passive = passive)
        static member inline onKeyPress (action: KeyboardEvent -> unit, ?passive: bool) = useWindowListener.on("keypress", action, ?passive = passive)
        static member inline onKeyUp (action: KeyboardEvent -> unit, ?passive: bool) = useWindowListener.on("keyup", action, ?passive = passive)
        static member inline onLanguageChange (action: Event -> unit, ?passive: bool) = useWindowListener.on("languagechange", action, ?passive = passive)
        static member inline onLoad (action: ProgressEvent -> unit, ?passive: bool) = useWindowListener.on("load", action, ?passive = passive)
        static member inline onLoad (action: UIEvent -> unit, ?passive: bool) = useWindowListener.on("load", action, ?passive = passive)
        static member inline onMessage (action: MessageEvent -> unit, ?passive: bool) = useWindowListener.on("message", action, ?passive = passive)
        static member inline onMessageError (action: MessageEvent -> unit, ?passive: bool) = useWindowListener.on("message", action, ?passive = passive)
        static member inline onMouseDown (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mousedown", action, ?passive = passive)
        static member inline onMouseEnter (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mouseenter", action, ?passive = passive)
        static member inline onMouseLeave (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mouseleave", action, ?passive = passive)
        static member inline onMouseMove (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mousemove", action, ?passive = passive)
        static member inline onMouseOut (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mouseout", action, ?passive = passive)
        static member inline onMouseOver (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mouseover", action, ?passive = passive)
        static member inline onMouseUp (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("mouseup", action, ?passive = passive)
        static member inline onOffline (action: Event -> unit, ?passive: bool) = useWindowListener.on("offline", action, ?passive = passive)
        static member inline onOnline (action: Event -> unit, ?passive: bool) = useWindowListener.on("online", action, ?passive = passive)
        static member inline onOrientationChange (action: Event -> unit, ?passive: bool) = useWindowListener.on("orientationchange", action, ?passive = passive)
        static member inline onPageHide (action: PageTransitionEvent -> unit, ?passive: bool) = useWindowListener.on("onPageHide", action, ?passive = passive)
        static member inline onPageShow (action: PageTransitionEvent -> unit, ?passive: bool) = useWindowListener.on("onPageShow", action, ?passive = passive)
        static member inline onPaste (action: ClipboardEvent -> unit, ?passive: bool) = useWindowListener.on("paste", action, ?passive = passive)
        static member inline onPopState (action: Event -> unit, ?passive: bool) = useWindowListener.on("popstate", action, ?passive = passive)
        static member inline onRejectionHandled (action: PromiseRejectionEvent -> unit, ?passive: bool) = useWindowListener.on("rejectionhandled", action, ?passive = passive)
        static member inline onReset (action: Event -> unit, ?passive: bool) = useWindowListener.on("reset", action, ?passive = passive)
        static member inline onResize (action: Event -> unit, ?passive: bool) = useWindowListener.on("resize", action, ?passive = passive)
        static member inline onScroll (action: Event -> unit, ?passive: bool) = useWindowListener.on("scroll", action, ?passive = passive)
        static member inline onSelect (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("select", action, ?passive = passive)
        static member inline onSelectionChange (action: Event -> unit, ?passive: bool) = useWindowListener.on("selectionchange", action, ?passive = passive)
        static member inline onSelectStart (action: Event -> unit, ?passive: bool) = useWindowListener.on("selectstart", action, ?passive = passive)
        static member inline onStorage (action: StorageEvent -> unit, ?passive: bool) = useWindowListener.on("storage", action, ?passive = passive)
        static member inline onSubmit (action: Event -> unit, ?passive: bool) = useWindowListener.on("submit", action, ?passive = passive)
        static member inline onTransitionCancel (action: TransitionEvent -> unit, ?passive: bool) = useWindowListener.on("transitioncancel", action, ?passive = passive)
        static member inline onTransitionEnd (action: TransitionEvent -> unit, ?passive: bool) = useWindowListener.on("transitionend", action, ?passive = passive)
        static member inline onTransitionRun (action: TransitionEvent -> unit, ?passive: bool) = useWindowListener.on("transitionrun", action, ?passive = passive)
        static member inline onTransitionStart (action: TransitionEvent -> unit, ?passive: bool) = useWindowListener.on("transitionstart", action, ?passive = passive)
        static member inline onUnhandledRejection (action: PromiseRejectionEvent -> unit, ?passive: bool) = useWindowListener.on("unhandledrejection", action, ?passive = passive)
        static member inline onUnload (action: Event -> unit, ?passive: bool) = useWindowListener.on("unload", action, ?passive = passive)
        static member inline onWheel (action: MouseEvent -> unit, ?passive: bool) = useWindowListener.on("wheel", action, ?passive = passive)
