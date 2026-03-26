import {math} from "../general/math.js";
import {elementManager} from "../general/elementManager.js";

export class overlayManager {
    static overlays = {}

    static controlMap = {
        "Hover": "mousemove",
        "Click": "click"
    }

    static rectContains(rect, x, y, i) {

        return rect.left - i < x && x < rect.right + i && rect.top - i < y && y < rect.bottom + i;
    }


    /**
     * @typedef {Object} OverlayOption
     * @property {string} controlType
     * @property {number} ith
     * @property {number} itv
     *
     * @typedef {Object} DotNetObjectReference<T>
     * @property {function} invokeMethodAsync
     *
     * @typedef {Object} JsInvoker
     *
     * replace the styles
     * @param {string} id
     * @param {HTMLElement} element
     * @param {HTMLElement} parent
     * @param {DotNetObjectReference<JsInvoker>} closeInvoker
     * @param {OverlayOption} options
     */
    static createOverlay(id, element, parent, closeInvoker, options) {
        this.deleteOverlay(id);
        element = elementManager.getElement(element);
        parent = elementManager.getElement(parent);
        let animId;
        let rectPre;

        function fixPosition() {
            let rect = parent.getBoundingClientRect();
            if (!math.rectEquals(rectPre, rect, 0.1)) {
                rectPre = rect;
                let style = element.style;
                style.setProperty("--ob", `${(rect.bottom).toFixed(2)}px`);
                style.setProperty("--ot", `${(rect.top).toFixed(2)}px`);
                style.setProperty("--ol", `${(rect.left).toFixed(2)}px`);
                style.setProperty("--or", `${(rect.right).toFixed(2)}px`);
                style.setProperty("--ow", `${(rect.width).toFixed(2)}px`);
                style.setProperty("--oh", `${(rect.height).toFixed(2)}px`);
            }
        }

        let overlay = {
            ...options,
            closeCheck: function (e) {
                let parentRect = parent.getBoundingClientRect();
                let point = {
                    x: e.clientX, y: e.clientY
                };
                if (math.rectContains(parentRect, point, overlay.ith, overlay.itv))
                    return;
                let rect = element.getBoundingClientRect();
                if (math.rectContains(rect, point, overlay.ith, overlay.itv))
                    return;

                closeInvoker.invokeMethodAsync("invoke");
            },
            interval: setInterval(function () {
                if (animId) {
                    cancelAnimationFrame(animId);
                }
                animId = requestAnimationFrame(function () {
                    fixPosition();
                    animId = null;
                })
            }, 20)
        };

        fixPosition();
        const event = this.controlMap[overlay.controlType];
        if (event)
            document.addEventListener(event, overlay.closeCheck);

        this.overlays[id] = overlay;
    }

    static deleteOverlay(id) {
        let overlay = this.overlays[id];
        if (!overlay) return;

        delete this.overlays[id];
        const event = this.controlMap[overlay.controlType];
        if (event)
            document.removeEventListener(event, overlay.closeCheck);
        clearInterval(overlay.interval);
    }
}