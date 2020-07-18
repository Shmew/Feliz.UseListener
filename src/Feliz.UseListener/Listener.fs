namespace Feliz.UseListener

open Browser.Types
open Browser.Dom
open Fable.Core
open Fable.Core.JsInterop
open Feliz

[<Erase;RequireQualifiedAccess>]
module React =
    [<Erase>]
    type useListener =
        static member inline on (eventType: string, action: #Event -> unit) =
            let listener = React.useCallbackRef(fun () ->
                let fn = unbox<#Event> >> action
                document.addEventListener(eventType, fn, false)        
                React.createDisposable(fun () -> document.removeEventListener(eventType, fn))
            )
            
            React.useEffect(listener)

        static member inline onAbort (action: ProgressEvent -> unit) = useListener.on("abort", action)
        static member inline onAbort (action: UIEvent -> unit) = useListener.on("abort", action)
        static member inline onAnimationCancel (action: AnimationEvent -> unit) = useListener.on("animationcancel", action)
        static member inline onAnimationEnd (action: AnimationEvent -> unit) = useListener.on("animationend", action)
        static member inline onAnimationIteration (action: AnimationEvent -> unit) = useListener.on("animationiteration", action)
        static member inline onAnimationStart (action: AnimationEvent -> unit) = useListener.on("animationstart", action)
        static member inline onAuxClick (action: MouseEvent -> unit) = useListener.on("onauxclick", action)
        static member inline onBlur (action: FocusEvent -> unit) = useListener.on("blur", action)
        static member inline onCancel (action: Event -> unit) = useListener.on("oncancel", action)
        static member inline onCanPlay (action: Event -> unit) = useListener.on("canplay", action)
        static member inline onCanPlayThrough (action: Event -> unit) = useListener.on("canplaythrough", action)
        static member inline onChange (action: Event -> unit) = useListener.on("change", action)
        static member inline onClick (action: MouseEvent -> unit) = useListener.on("click", action)
        static member inline onClose (action: Event -> unit) = useListener.on("onclose", action)
        static member inline onContextMenu (action: MouseEvent -> unit) = useListener.on("contextmenu", action)
        static member inline onCopy (action: ClipboardEvent -> unit) = useListener.on("copy", action)
        static member inline onCueChange (action: Event -> unit) = useListener.on("oncuechange", action)
        static member inline onCut (action: ClipboardEvent -> unit) = useListener.on("cut", action)
        static member inline onDblClick (action: MouseEvent -> unit) = useListener.on("dblClick", action)
        static member inline onDOMContentLoaded (action: MouseEvent -> unit) = useListener.on("DOMContentLoaded", action)
        static member inline onDrag (action: DragEvent -> unit) = useListener.on("drag", action)
        static member inline onDragEnd (action: DragEvent -> unit) = useListener.on("dragend", action)
        static member inline onDragEnter (action: DragEvent -> unit) = useListener.on("dragenter", action)
        static member inline onDragExit (action: DragEvent -> unit) = useListener.on("dragexit", action)
        static member inline onDragLeave (action: DragEvent -> unit) = useListener.on("dragleave", action)
        static member inline onDragOver (action: DragEvent -> unit) = useListener.on("dragover", action)
        static member inline onDragStart (action: DragEvent -> unit) = useListener.on("dragstart", action)
        static member inline onDrop (action: DragEvent -> unit) = useListener.on("drop", action)
        static member inline onDurationChange (action: Event -> unit) = useListener.on("durationchange", action)
        static member inline onEmptied (action: Event -> unit) = useListener.on("emptied", action)
        static member inline onEnded (action: Event -> unit) = useListener.on("ended", action)
        static member inline onError (action: ProgressEvent -> unit) = useListener.on("error", action)
        static member inline onError (action: UIEvent -> unit) = useListener.on("error", action)
        static member inline onFocus (action: FocusEvent -> unit) = useListener.on("focus", action)
        static member inline onFormData (action: Event -> unit) = useListener.on("formdata", action)
        static member inline onFullscreenChange (action: Event -> unit) = useListener.on("fullscreenchange", action)
        static member inline onFullscreenError (action: Event -> unit) = useListener.on("fullscreenerror", action)
        static member inline onGotPointerCapture (action: PointerEvent -> unit) = useListener.on("gotPointercapture", action)
        static member inline onInput (action: UIEvent -> unit) = useListener.on("input", action)
        static member inline onInvalid (action: Event -> unit) = useListener.on("invalid", action)
        static member inline onKeyDown (action: KeyboardEvent -> unit) = useListener.on("keydown", action)
        static member inline onKeyPress (action: KeyboardEvent -> unit) = useListener.on("keypress", action)
        static member inline onKeyUp (action: KeyboardEvent -> unit) = useListener.on("keyup", action)
        static member inline onLoad (action: ProgressEvent -> unit) = useListener.on("load", action)
        static member inline onLoad (action: UIEvent -> unit) = useListener.on("load", action)
        static member inline onLoadedData (action: Event -> unit) = useListener.on("loadeddata", action)
        static member inline onLoadedMetadata (action: Event -> unit) = useListener.on("loadedmetadata", action)
        static member inline onLoadEnd (action: ProgressEvent -> unit) = useListener.on("loadend", action)
        static member inline onLoadStart (action: ProgressEvent -> unit) = useListener.on("loadstart", action)
        static member inline onLostPointerCapture (action: PointerEvent -> unit) = useListener.on("lostpointercapture", action)
        static member inline onMouseDown (action: MouseEvent -> unit) = useListener.on("mousedown", action)
        static member inline onMouseEnter (action: MouseEvent -> unit) = useListener.on("mouseenter", action)
        static member inline onMouseLeave (action: MouseEvent -> unit) = useListener.on("mouseleave", action)
        static member inline onMouseMove (action: MouseEvent -> unit) = useListener.on("mousemove", action)
        static member inline onMouseOut (action: MouseEvent -> unit) = useListener.on("mouseout", action)
        static member inline onMouseOver (action: MouseEvent -> unit) = useListener.on("mouseover", action)
        static member inline onMouseUp (action: MouseEvent -> unit) = useListener.on("mouseup", action)
        static member inline onPaste (action: ClipboardEvent -> unit) = useListener.on("paste", action)
        static member inline onPause (action: Event -> unit) = useListener.on("pause", action)
        static member inline onPlay (action: Event -> unit) = useListener.on("play", action)
        static member inline onPlaying (action: Event -> unit) = useListener.on("playing", action)
        static member inline onPointerCancel (action: PointerEvent -> unit) = useListener.on("pointercancel", action)
        static member inline onPointerDown (action: PointerEvent -> unit) = useListener.on("pointerdown", action)
        static member inline onPointerEnter (action: PointerEvent -> unit) = useListener.on("pointerenter", action)
        static member inline onPointerLeave (action: PointerEvent -> unit) = useListener.on("pointerleave", action)
        static member inline onPointerMove (action: PointerEvent -> unit) = useListener.on("pointermove", action)
        static member inline onPointerOut (action: PointerEvent -> unit) = useListener.on("pointerout", action)
        static member inline onPointerOver (action: PointerEvent -> unit) = useListener.on("pointerover", action)
        static member inline onPointerUp (action: PointerEvent -> unit) = useListener.on("pointerup", action)
        static member inline onProgress (action: ProgressEvent -> unit) = useListener.on("progress", action)
        static member inline onRateChange (action: Event -> unit) = useListener.on("ratechange", action)
        static member inline onReadyStateChange (action: Event -> unit) = useListener.on("readystatechange", action)
        static member inline onReset (action: Event -> unit) = useListener.on("reset", action)
        static member inline onResize (action: Event -> unit) = useListener.on("resize", action)
        static member inline onScroll (action: Event -> unit) = useListener.on("scroll", action)
        static member inline onSeeked (action: Event -> unit) = useListener.on("seeked", action)
        static member inline onSeeking (action: Event -> unit) = useListener.on("seeking", action)
        static member inline onSelect (action: MouseEvent -> unit) = useListener.on("select", action)
        static member inline onSelectionChange (action: Event -> unit) = useListener.on("selectionchange", action)
        static member inline onSelectStart (action: Event -> unit) = useListener.on("selectstart", action)
        static member inline onStalled (action: Event -> unit) = useListener.on("stalled", action)
        static member inline onSubmit (action: Event -> unit) = useListener.on("submit", action)
        static member inline onSuspend (action: Event -> unit) = useListener.on("suspend", action)
        static member inline onTimeUpdate (action: Event -> unit) = useListener.on("timeupdate", action)
        static member inline onTouchCancel (action: TouchEvent -> unit) = useListener.on("touchcancel", action)
        static member inline onTouchEnd (action: TouchEvent -> unit) = useListener.on("touchend", action)
        static member inline onTouchMove (action: TouchEvent -> unit) = useListener.on("touchmove", action)
        static member inline onTouchStart (action: TouchEvent -> unit) = useListener.on("touchstart", action)
        static member inline onTransitionCancel (action: TransitionEvent -> unit) = useListener.on("transitioncancel", action)
        static member inline onTransitionEnd (action: TransitionEvent -> unit) = useListener.on("transitionend", action)
        static member inline onTransitionRun (action: TransitionEvent -> unit) = useListener.on("transitionrun", action)
        static member inline onTransitionStart (action: TransitionEvent -> unit) = useListener.on("transitionstart", action)
        static member inline onVisibilityChange (action: Event -> unit) = useListener.on("visibilitychange", action)
        static member inline onVolumeChange (action: Event -> unit) = useListener.on("volumechange", action)
        static member inline onWaiting (action: Event -> unit) = useListener.on("waiting", action)
        static member inline onWheel (action: MouseEvent -> unit) = useListener.on("wheel", action)

        /// Invokes the callback when a click event is not within the given element.
        static member inline onClickAway (elemRef: IRefValue<#HTMLElement option>, callback: MouseEvent -> unit) =
            useListener.onMouseDown(fun ev ->
                match elemRef.current with
                | Some elem when not (elem.contains(unbox ev.target)) ->
                    callback ev
                | _ -> ()
            )

    [<Erase>]
    type useElementListener =
        static member inline on (elemRef: IRefValue<#HTMLElement option>, eventType: string, action: #Event -> unit) =
            let listener = React.useCallbackRef(fun () ->
                let fn = unbox<#Event> >> action
                elemRef.current |> Option.iter(fun elem ->
                    elem.addEventListener(eventType, fn, false)
                )

                React.createDisposable(fun () -> 
                    elemRef.current |> Option.iter(fun elem ->
                        elem.removeEventListener(eventType, fn)
                ))
            )

            React.useEffect(listener)

        static member inline onAbort (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "abort", action)
        static member inline onAbort (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit) = useElementListener.on(elemRef, "abort", action)
        static member inline onAnimationCancel (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit) = useElementListener.on(elemRef, "animationcancel", action)
        static member inline onAnimationEnd (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit) = useElementListener.on(elemRef, "animationend", action)
        static member inline onAnimationIteration (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit) = useElementListener.on(elemRef, "animationiteration", action)
        static member inline onAnimationStart (elemRef: IRefValue<#HTMLElement option>, action: AnimationEvent -> unit) = useElementListener.on(elemRef, "animationstart", action)
        static member inline onAuxClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "onauxclick", action)
        static member inline onBlur (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit) = useElementListener.on(elemRef, "blur", action)
        static member inline onCancel (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "oncancel", action)
        static member inline onCanPlay (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "canplay", action)
        static member inline onCanPlayThrough (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "canplaythrough", action)
        static member inline onChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "change", action)
        static member inline onClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "click", action)
        static member inline onClose (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "onclose", action)
        static member inline onContextMenu (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "contextmenu", action)
        static member inline onCopy (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit) = useElementListener.on(elemRef, "copy", action)
        static member inline onCueChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "oncuechange", action)
        static member inline onCut (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit) = useElementListener.on(elemRef, "cut", action)
        static member inline onDblClick (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "dblClick", action)
        static member inline onDOMContentLoaded (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "DOMContentLoaded", action)
        static member inline onDrag (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "drag", action)
        static member inline onDragEnd (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragend", action)
        static member inline onDragEnter (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragenter", action)
        static member inline onDragExit (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragexit", action)
        static member inline onDragLeave (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragleave", action)
        static member inline onDragOver (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragover", action)
        static member inline onDragStart (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "dragstart", action)
        static member inline onDrop (elemRef: IRefValue<#HTMLElement option>, action: DragEvent -> unit) = useElementListener.on(elemRef, "drop", action)
        static member inline onDurationChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "durationchange", action)
        static member inline onEmptied (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "emptied", action)
        static member inline onEnded (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "ended", action)
        static member inline onError (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "error", action)
        static member inline onError (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit) = useElementListener.on(elemRef, "error", action)
        static member inline onFocus (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit) = useElementListener.on(elemRef, "focus", action)
        static member inline onFocusIn (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit) = useElementListener.on(elemRef, "focusin", action)
        static member inline onFocusOut (elemRef: IRefValue<#HTMLElement option>, action: FocusEvent -> unit) = useElementListener.on(elemRef, "focusout", action)
        static member inline onFormData (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "formdata", action)
        static member inline onFullscreenChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "fullscreenchange", action)
        static member inline onFullscreenError (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "fullscreenerror", action)
        static member inline onGotPointerCapture (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "gotPointercapture", action)
        static member inline onInput (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit) = useElementListener.on(elemRef, "input", action)
        static member inline onInvalid (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "invalid", action)
        static member inline onKeyDown (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit) = useElementListener.on(elemRef, "keydown", action)
        static member inline onKeyPress (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit) = useElementListener.on(elemRef, "keypress", action)
        static member inline onKeyUp (elemRef: IRefValue<#HTMLElement option>, action: KeyboardEvent -> unit) = useElementListener.on(elemRef, "keyup", action)
        static member inline onLoad (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "load", action)
        static member inline onLoad (elemRef: IRefValue<#HTMLElement option>, action: UIEvent -> unit) = useElementListener.on(elemRef, "load", action)
        static member inline onLoadedData (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "loadeddata", action)
        static member inline onLoadedMetadata (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "loadedmetadata", action)
        static member inline onLoadEnd (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "loadend", action)
        static member inline onLoadStart (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "loadstart", action)
        static member inline onLostPointerCapture (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "lostpointercapture", action)
        static member inline onMouseDown (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mousedown", action)
        static member inline onMouseEnter (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mouseenter", action)
        static member inline onMouseLeave (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mouseleave", action)
        static member inline onMouseMove (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mousemove", action)
        static member inline onMouseOut (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mouseout", action)
        static member inline onMouseOver (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mouseover", action)
        static member inline onMouseUp (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "mouseup", action)
        static member inline onPaste (elemRef: IRefValue<#HTMLElement option>, action: ClipboardEvent -> unit) = useElementListener.on(elemRef, "paste", action)
        static member inline onPause (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "pause", action)
        static member inline onPlay (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "play", action)
        static member inline onPlaying (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "playing", action)
        static member inline onPointerCancel (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointercancel", action)
        static member inline onPointerDown (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerdown", action)
        static member inline onPointerEnter (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerenter", action)
        static member inline onPointerLeave (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerleave", action)
        static member inline onPointerMove (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointermove", action)
        static member inline onPointerOut (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerout", action)
        static member inline onPointerOver (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerover", action)
        static member inline onPointerUp (elemRef: IRefValue<#HTMLElement option>, action: PointerEvent -> unit) = useElementListener.on(elemRef, "pointerup", action)
        static member inline onProgress (elemRef: IRefValue<#HTMLElement option>, action: ProgressEvent -> unit) = useElementListener.on(elemRef, "progress", action)
        static member inline onRateChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "ratechange", action)
        static member inline onReadyStateChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "readystatechange", action)
        static member inline onReset (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "reset", action)
        static member inline onResize (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "resize", action)
        static member inline onScroll (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "scroll", action)
        static member inline onSeeked (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "seeked", action)
        static member inline onSeeking (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "seeking", action)
        static member inline onSelect (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "select", action)
        static member inline onSelectionChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "selectionchange", action)
        static member inline onSelectStart (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "selectstart", action)
        static member inline onStalled (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "stalled", action)
        static member inline onSubmit (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "submit", action)
        static member inline onSuspend (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "suspend", action)
        static member inline onTimeUpdate (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "timeupdate", action)
        static member inline onTouchCancel (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit) = useElementListener.on(elemRef, "touchcancel", action)
        static member inline onTouchEnd (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit) = useElementListener.on(elemRef, "touchend", action)
        static member inline onTouchMove (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit) = useElementListener.on(elemRef, "touchmove", action)
        static member inline onTouchStart (elemRef: IRefValue<#HTMLElement option>, action: TouchEvent -> unit) = useElementListener.on(elemRef, "touchstart", action)
        static member inline onTransitionCancel (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit) = useElementListener.on(elemRef, "transitioncancel", action)
        static member inline onTransitionEnd (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit) = useElementListener.on(elemRef, "transitionend", action)
        static member inline onTransitionRun (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit) = useElementListener.on(elemRef, "transitionrun", action)
        static member inline onTransitionStart (elemRef: IRefValue<#HTMLElement option>, action: TransitionEvent -> unit) = useElementListener.on(elemRef, "transitionstart", action)
        static member inline onVisibilityChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "visibilitychange", action)
        static member inline onVolumeChange (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "volumechange", action)
        static member inline onWaiting (elemRef: IRefValue<#HTMLElement option>, action: Event -> unit) = useElementListener.on(elemRef, "waiting", action)
        static member inline onWheel (elemRef: IRefValue<#HTMLElement option>, action: MouseEvent -> unit) = useElementListener.on(elemRef, "wheel", action)

    [<Erase>]
    type useStyle =
        static member inline onCustom (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) (resolver: #HTMLElement option -> bool) =
            let isTrue,setIsTrue = React.useState false
            
            useElementListener.onChange(elemRef, fun _ ->
                elemRef.current
                |> resolver
                |> setIsTrue
            )

            React.useMemo((fun () -> 
                if isTrue then styles
                else []
            ), [| isTrue :> obj; styles :> obj |])

        static member inline onClick (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) =
            let isClicked,setIsClicked = React.useState false
            
            useElementListener.onClick(elemRef, fun _ -> setIsClicked (not isClicked))
            
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
            
            useElementListener.onFocusIn(elemRef, fun _ -> setIsFocused true)
            useElementListener.onFocusOut(elemRef, fun _ -> setIsFocused false)

            React.useMemo((fun () -> 
                if isFocused then styles
                else []
            ), [| isFocused :> obj; styles :> obj |])

        static member inline onHover (elemRef: IRefValue<#HTMLElement option>) (styles: IStyleAttribute list) =
            let isHovered,setIsHovered = React.useState false
            
            useElementListener.onMouseEnter(elemRef, fun _ -> setIsHovered true)
            useElementListener.onMouseLeave(elemRef, fun _ -> setIsHovered false)

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
        static member inline on (eventType: string, action: #Event -> unit) =
            let listener = React.useCallbackRef(fun () ->
                let fn = unbox<#Event> >> action
                window.addEventListener(eventType, fn, false)        
                React.createDisposable(fun () -> window.removeEventListener(eventType, fn))
            )
            
            React.useEffect(listener)
        
        static member inline onAbort (action: ProgressEvent -> unit) = useWindowListener.on("abort", action)
        static member inline onAbort (action: UIEvent -> unit) = useWindowListener.on("abort", action)
        static member inline onAnimationCancel (action: AnimationEvent -> unit) = useWindowListener.on("animationcancel", action)
        static member inline onAnimationEnd (action: AnimationEvent -> unit) = useWindowListener.on("animationend", action)
        static member inline onAnimationIteration (action: AnimationEvent -> unit) = useWindowListener.on("animationiteration", action)
        static member inline onAnimationStart (action: AnimationEvent -> unit) = useWindowListener.on("animationstart", action)
        static member inline onAuxClick (action: MouseEvent -> unit) = useWindowListener.on("onauxclick", action)
        static member inline onAfterPrint (action: Event -> unit) = useWindowListener.on("afterprint", action)
        static member inline onBeforeInstallPrompt (action: Event -> unit) = useWindowListener.on("beforeinstallprompt", action)
        static member inline onBeforePrint (action: Event -> unit) = useWindowListener.on("beforeprint", action)
        static member inline onBeforeUnload (action: Event -> unit) = useWindowListener.on("beforeunload", action)
        static member inline onBlur (action: FocusEvent -> unit) = useWindowListener.on("blur", action)
        static member inline onChange (action: Event -> unit) = useWindowListener.on("change", action)
        static member inline onClick (action: MouseEvent -> unit) = useWindowListener.on("click", action)
        static member inline onClipboardChange (action: MouseEvent -> unit) = useWindowListener.on("clipboardchange", action)
        static member inline onClose (action: Event -> unit) = useWindowListener.on("onclose", action)
        static member inline onContextMenu (action: MouseEvent -> unit) = useWindowListener.on("contextmenu", action)
        static member inline onCopy (action: ClipboardEvent -> unit) = useWindowListener.on("copy", action)
        static member inline onCut (action: ClipboardEvent -> unit) = useWindowListener.on("cut", action)
        static member inline onDblClick (action: MouseEvent -> unit) = useWindowListener.on("dblClick", action)
        static member inline onDeviceMotion (action: DeviceMotionEvent -> unit) = useWindowListener.on("devicemotion", action)
        static member inline onDeviceOrientation (action: DeviceOrientationEvent -> unit) = useWindowListener.on("deviceorientation", action)
        static member inline onDOMContentLoaded (action: MouseEvent -> unit) = useWindowListener.on("DOMContentLoaded", action)
        static member inline onDurationChange (action: Event -> unit) = useWindowListener.on("durationchange", action)
        static member inline onError (action: ProgressEvent -> unit) = useWindowListener.on("error", action)
        static member inline onError (action: UIEvent -> unit) = useWindowListener.on("error", action)
        static member inline onFocus (action: FocusEvent -> unit) = useWindowListener.on("focus", action)
        static member inline onHashChange (action: HashChangeEvent -> unit) = useWindowListener.on("hashchange", action)
        static member inline onInput (action: UIEvent -> unit) = useWindowListener.on("input", action)
        static member inline onInvalid (action: Event -> unit) = useWindowListener.on("invalid", action)
        static member inline onKeyDown (action: KeyboardEvent -> unit) = useWindowListener.on("keydown", action)
        static member inline onKeyPress (action: KeyboardEvent -> unit) = useWindowListener.on("keypress", action)
        static member inline onKeyUp (action: KeyboardEvent -> unit) = useWindowListener.on("keyup", action)
        static member inline onLanguageChange (action: Event -> unit) = useWindowListener.on("languagechange", action)
        static member inline onLoad (action: ProgressEvent -> unit) = useWindowListener.on("load", action)
        static member inline onLoad (action: UIEvent -> unit) = useWindowListener.on("load", action)
        static member inline onMessage (action: MessageEvent -> unit) = useWindowListener.on("message", action)
        static member inline onMessageError (action: MessageEvent -> unit) = useWindowListener.on("message", action)
        static member inline onMouseDown (action: MouseEvent -> unit) = useWindowListener.on("mousedown", action)
        static member inline onMouseEnter (action: MouseEvent -> unit) = useWindowListener.on("mouseenter", action)
        static member inline onMouseLeave (action: MouseEvent -> unit) = useWindowListener.on("mouseleave", action)
        static member inline onMouseMove (action: MouseEvent -> unit) = useWindowListener.on("mousemove", action)
        static member inline onMouseOut (action: MouseEvent -> unit) = useWindowListener.on("mouseout", action)
        static member inline onMouseOver (action: MouseEvent -> unit) = useWindowListener.on("mouseover", action)
        static member inline onMouseUp (action: MouseEvent -> unit) = useWindowListener.on("mouseup", action)
        static member inline onOffline (action: Event -> unit) = useWindowListener.on("offline", action)
        static member inline onOnline (action: Event -> unit) = useWindowListener.on("online", action)
        static member inline onOrientationChange (action: Event -> unit) = useWindowListener.on("orientationchange", action)
        static member inline onPageHide (action: PageTransitionEvent -> unit) = useWindowListener.on("onPageHide", action)
        static member inline onPageShow (action: PageTransitionEvent -> unit) = useWindowListener.on("onPageShow", action)
        static member inline onPaste (action: ClipboardEvent -> unit) = useWindowListener.on("paste", action)
        static member inline onPopState (action: Event -> unit) = useWindowListener.on("popstate", action)
        static member inline onRejectionHandled (action: PromiseRejectionEvent -> unit) = useWindowListener.on("rejectionhandled", action)
        static member inline onReset (action: Event -> unit) = useWindowListener.on("reset", action)
        static member inline onResize (action: Event -> unit) = useWindowListener.on("resize", action)
        static member inline onScroll (action: Event -> unit) = useWindowListener.on("scroll", action)
        static member inline onSelect (action: MouseEvent -> unit) = useWindowListener.on("select", action)
        static member inline onSelectionChange (action: Event -> unit) = useWindowListener.on("selectionchange", action)
        static member inline onSelectStart (action: Event -> unit) = useWindowListener.on("selectstart", action)
        static member inline onStorage (action: StorageEvent -> unit) = useWindowListener.on("storage", action)
        static member inline onSubmit (action: Event -> unit) = useWindowListener.on("submit", action)
        static member inline onTransitionCancel (action: TransitionEvent -> unit) = useWindowListener.on("transitioncancel", action)
        static member inline onTransitionEnd (action: TransitionEvent -> unit) = useWindowListener.on("transitionend", action)
        static member inline onTransitionRun (action: TransitionEvent -> unit) = useWindowListener.on("transitionrun", action)
        static member inline onTransitionStart (action: TransitionEvent -> unit) = useWindowListener.on("transitionstart", action)
        static member inline onUnhandledRejection (action: PromiseRejectionEvent -> unit) = useWindowListener.on("unhandledrejection", action)
        static member inline onUnload (action: Event -> unit) = useWindowListener.on("unload", action)
        static member inline onWheel (action: MouseEvent -> unit) = useWindowListener.on("wheel", action)
