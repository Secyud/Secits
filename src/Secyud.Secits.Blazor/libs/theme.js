

export function replaceStyles(styles) {
    let links = document.querySelectorAll('link[theme="secits"]');

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
            href = href?.replace(href.substring(href.indexOf('?_v')), '');
            if (href !== path) {
                link.href = path;
            }
            delete linkDict[id];
        } else {
            let link = document.createElement('link');
            link.type = 'text/css';
            link.rel = 'stylesheet';
            link.id = id;
            link.theme = 'secits';
            document.getElementsByTagName('head')[0].appendChild(link);
        }
    }

    for (const key in linkDict) {
        let link = linkDict[key];
        link.remove();
    }
}