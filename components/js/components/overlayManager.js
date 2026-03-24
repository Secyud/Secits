import {math} from "../general/math.js";

const interval = 4;

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
                style.setProperty("--ob", `${(rect.bottom - interval).toFixed(2)}px`);
                style.setProperty("--ot", `${(rect.top - interval).toFixed(2)}px`);
                style.setProperty("--ol", `${(rect.left - interval).toFixed(2)}px`);
                style.setProperty("--or", `${(rect.right - interval).toFixed(2)}px`);
                style.setProperty("--ow", `${(rect.width + interval * 2).toFixed(2)}px`);
                style.setProperty("--oh", `${(rect.height + interval * 2).toFixed(2)}px`);
                style.setProperty("--oi", `4px`);
            }
        }

        let overlay = {
            controlType,
            closeCheck: function (e) {
                let parentRect = parent.getBoundingClientRect();
                let point = {
                    x: e.clientX, y: e.clientY
                };
                if (math.rectContains(parentRect, point, interval))
                    return;
                let rect = element.getBoundingClientRect();
                if (math.rectContains(rect, point, interval))
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