import {elementManager} from "./elementManager.js";

export class eventManager {
    static totalEvents = {};

    static create(element, eventName, invoker, preventDefault = false, stopPropagation = false, delay = 0) {
        const callback = args => {
            const obj = {};
            for (const k in args) {
                const v = args[k];
                if (k === 'originalTarget' ||
                    typeof v === 'function' ||
                    typeof v === 'object' ||
                    v instanceof Node ||
                    v instanceof Window) {
                    continue;
                }
                const key = k[0].toUpperCase() + k.substring(1);
                obj[key] = v;
            }

            setTimeout(function () {
                invoker.invokeMethodAsync('invoke', obj)
            }, delay);

            if (preventDefault === true) {
                args.preventDefault();
            }
            if (stopPropagation) {
                args.stopPropagation();
            }
        };

        const dom = elementManager.getElement(element);

        if (dom && dom.addEventListener) {
            const key = this.getKey(eventName, invoker);
            const previous = this.totalEvents[key];
            if (previous) {
                dom.removeEventListener(eventName, previous);
            }
            this.totalEvents[key] = callback;
            dom.addEventListener(eventName, callback);
        }
    }

    static delete(element, eventName, invoker) {
        const dom = elementManager.getElement(element);

        if (dom && dom.addEventListener) {
            const key = this.getKey(eventName, invoker);
            const previous = this.totalEvents[key];
            if (previous) {
                dom.removeEventListener(eventName, previous);
            }
        }
    }

    static getKey(eventName, invoker) {
        return `e_${eventName}-${invoker._id}`;
    }
}