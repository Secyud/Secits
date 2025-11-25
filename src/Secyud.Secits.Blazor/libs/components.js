import {replaceStyles} from "./theme.js"
import Cookies from "js-cookie"

window.invokeElementMethod = function (element, method, ...params) {
    return element[method](...params);
}

window.invokeElementMethodVoid = function (element, method, ...params) {
    element[method](...params);
}

window.setCurrentStyle = function (styleName, styles) {
    Cookies.set('secits-theme', styleName);
    replaceStyles(styles);
}