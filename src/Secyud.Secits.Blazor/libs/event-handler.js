export const getDocumentEventHandler = function () {
    let record = 0;
    const allEventListener = {};
    const generateFunc = function (invoker) {
        const m = function (e) {
            return {
                Detail: e.detail,
                ScreenX: e.screenX,
                ScreenY: e.screenY,
                ClientX: e.clientX,
                ClientY: e.clientY,
                OffsetX: e.offsetX,
                OffsetY: e.offsetY,
                PageX: e.pageX,
                PageY: e.pageY,
                MovementX: e.movementX,
                MovementY: e.movementY,
                Button: e.button,
                Buttons: e.buttons,
                CtrlKey: e.ctrlKey,
                ShiftKey: e.shiftKey,
                AltKey: e.altKey,
                MetaKey: e.metaKey,
                Type: e.type,
            }
        }

        function res(e) {
            invoker.invokeMethodAsync('Invoke', m(e));
        }

        return res;
    }


    return {
        addEventListener: function (invoker, types) {
            let id = (record++) % 0xffffffff;
            let listener = {
                func: generateFunc(invoker), types
            };
            for (const type of listener.types) {
                document.addEventListener(type, listener.func, false);
            }
            allEventListener[id] = listener;
            return id;
        },
        removeEventListener: function (id) {
            let listener = allEventListener[id];
            if (listener) {
                for (const type of listener.types) {
                    document.removeEventListener(type, listener.func, false);
                }
                delete allEventListener[id];
            }
        }
    }
}