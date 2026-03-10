export class elementManager {
    static invoke(element, method, ...params) {
        return element[method](...params);
    }

    static invokeVoid(element, method, ...params) {
        element[method](...params);
    }

}