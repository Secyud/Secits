export class elementManager {
    static invoke(element, method, ...params) {
        return this.getElement(element)[method](...params);
    }

    static invokeVoid(element, method, ...params) {
        this.getElement(element)[method](...params);
    }

    static setProperty(element, property, value) {
        this.getElement(element)[property] = value;
    }

    static getElement(element) {
        if (!element) {
            element = document.body;
        } else if (typeof element === 'string') {
            if (element === 'window') {
                return window;
            } else if (element === 'document') {
                return document;
            }
            element = document.querySelector(element);
        }
        return element;
    }
}