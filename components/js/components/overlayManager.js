import {math} from "../general/math.js";

export class overlayManager {
    static overlays = {}

    static controlMap = {
        "Hover": "mousemove",
        "Click": "click"
    }

    static rectContains(rect, x, y, i) {

        return rect.left - i < x && x < rect.right + i && rect.top - i < y && y < rect.bottom + i;
    }

    static createOverlay(id, element, parent, controlType, closeInvoker) {
        this.deleteOverlay(id);
        let animId;
        let rectPre;

        function fixPosition() {
            let rect = parent.getBoundingClientRect();
            if (!math.rectEquals(rectPre, rect, 0.1)) {
                rectPre = rect;
                let style = element.style;
                style.setProperty("--ob", `${(rect.bottom - 4).toFixed(2)}px`);
                style.setProperty("--ot", `${(rect.top - 4).toFixed(2)}px`);
                style.setProperty("--ol", `${(rect.left - 4).toFixed(2)}px`);
                style.setProperty("--or", `${(rect.right - 4).toFixed(2)}px`);
                style.setProperty("--ow", `${(rect.width + 8).toFixed(2)}px`);
                style.setProperty("--oh", `${(rect.height + 8).toFixed(2)}px`);
                style.setProperty("--oi", `4px`);
            }
        }

        let overlay = {
            controlType,
            closeCheck: function (e) {
                let parentRect = parent.getBoundingClientRect();
                if (math.rectContains(parentRect, e.clientX, e.clientY, 4))
                    return;
                let rect = element.getBoundingClientRect();
                if (math.rectContains(rect, e.clientX, e.clientY, 4))
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
        document.addEventListener(this.controlMap[controlType], overlay.closeCheck);

        this.overlays[id] = overlay;
    }

    static deleteOverlay(id) {
        let overlay = this.overlays[id];
        if (!overlay) return;

        delete this.overlays[id];
        document.removeEventListener(this.controlMap[overlay.controlType], overlay.closeCheck);
        clearInterval(overlay.interval);
    }
}