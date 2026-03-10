import Cookies from "js-cookie";

export class themeManager {

    /**
     * @typedef {Object} Style
     * @property {string} id
     * @property {string} path
     *
     * replace the styles
     * @param {Style[]} styles
     */
    static replaceStyles(styles) {
        // get all CSS imported by secits
        let links = document.querySelectorAll('link[general="secits"]');

        // analyze all styles imported
        let linkDict = {};
        for (const link of links) {
            let id = link.getAttribute('id');
            linkDict[id] = link;
        }

        for (const style of styles) {
            let id = style.id;
            let path = style.path;

            if (linkDict[id]) {
                let link = linkDict[id];
                let href = link.getAttribute('href');
                if (href !== path) {
                    link.href = path;
                }
                // remove from delete list
                delete linkDict[id];
            } else {
                // new CSS
                let link = document.createElement('link');
                link.type = 'text/css';
                link.rel = 'stylesheet';
                link.id = id;
                link.theme = 'secits';
                document.getElementsByTagName('head')[0].appendChild(link);
            }
        }

        // delete CSS not imported
        for (const key in linkDict) {
            let link = linkDict[key];
            link.remove();
        }
    }

    static setCurrentStyle(styleName, styles) {
        Cookies.set('secits-general', styleName);
        this.replaceStyles(styles);
    }
}

