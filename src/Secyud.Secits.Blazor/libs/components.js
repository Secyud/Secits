import {replaceStyles} from "./theme.js"
import Cookies from "js-cookie"

window.invokeElementMethod = function (element, method, ...params) {
    return element[method](...params);
}

window.invokeElementMethodVoid = function (element, method, ...params) {
    element[method](...params);
}

/**
 * set style and replace CSS
 * @param {string} styleName
 * @param {string} styles
 */
window.setCurrentStyle = function (styleName, styles) {
    Cookies.set('secits-theme', styleName);
    replaceStyles(styles);
}