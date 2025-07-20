export const svgEditor = {
    createCanvas(viewPort = {x: 0, y: 0, width: 256, height: 256}, precision = 1) {
        let canvas = {
            shapes: [], viewPort: viewPort, precision: precision, generateSvg() {
                return svgEditor.generateSvg(canvas);
            }
        };
        return canvas;
    }, generateSvg(canvas) {
        let sb = new SvgBuilder(canvas.precision);

        sb.append('<svg xmlns="http://www.w3.org/2000/svg" viewBox="');
        sb.appendJoin(' ', -canvas.viewPort.x, -canvas.viewPort.y, canvas.viewPort.width, canvas.viewPort.height);
        sb.append('">');

        for (const shape of canvas.shapes) {
            switch (shape.name) {
                case 'path': {
                    sb.append('<path d="M');
                    let startPoint = {
                        x: shape.startPoint.x ?? 0,
                        y: shape.startPoint.y ?? 0,
                    }
                    sb.appendFix(startPoint.x);
                    sb.appendFixP(startPoint.y);
                    let process = {
                        points: [startPoint], steps: {}
                    };

                    for (const step of shape.steps) {
                        let nodes;
                        switch (step.name) {
                            case "draw":
                                let points = process.points;
                                let point = {...points[points.length - 1]};
                                let start = {
                                    point: point,
                                }
                                let next = start;
                                for (const draw of step.draws) {
                                    let connection = {...draw};
                                    next.nextConnection = connection;
                                    point = draw.isRelative ? {
                                        x: (draw.point.x ?? 0) + point.x, y: (draw.point.y ?? 0) + point.y,
                                    } : {
                                        x: (draw.point.x ?? 0), y: (draw.point.y ?? 0),
                                    };
                                    next = {
                                        point: point
                                    };
                                    connection.nextNode = next;
                                }
                                nodes = {start: start};
                                break;
                            case "rotate":
                                nodes = transforms.rotate(process.steps[step.refId], step);
                                break;
                            case "mirror":
                                nodes = transforms.mirror(process.steps[step.refId], step);
                                break;
                            case "reverse":
                                nodes = transforms.reverse(process.steps[step.refId], step);
                                break;
                            default:
                                break;
                        }
                        process.steps[step.id] = nodes;
                        if (step.disabled) continue;
                        let node = nodes.start;
                        let connection = node.nextConnection;
                        while (connection) {
                            let nextNode = connection.nextNode;
                            let point = nextNode.point;
                            process.points.push(point);
                            sb.append(connection.name);
                            switch (connection.name) {
                                case "M":
                                case "L":
                                case "T": {
                                    sb.appendFix(point.x);
                                    sb.appendFixP(point.y);
                                    break;
                                }
                                case "C": {
                                    let point1 = connection.point1;
                                    sb.appendFix(point1.x);
                                    sb.appendFixP(point1.y);

                                    let point2 = connection.point2;
                                    sb.appendFixP(point2.x);
                                    sb.appendFixP(point2.y);

                                    sb.appendFixP(point.x);
                                    sb.appendFixP(point.y);
                                    break;
                                }
                                case "S":
                                case "Q": {
                                    let point1 = connection.point1;

                                    sb.appendFix(point1.x);
                                    sb.appendFixP(point1.y);

                                    sb.appendFixP(point.x);
                                    sb.appendFixP(point.y);
                                    break;
                                }
                                case "H": {
                                    sb.appendFix(point.x);
                                    break;
                                }
                                case "V": {
                                    sb.appendFix(point.y);
                                    break;
                                }
                                case "A": {
                                    sb.appendFix(connection.rx);
                                    sb.appendFixP(connection.ry);
                                    sb.appendFixP(connection.xr);
                                    sb.appendFixP(connection.laf);
                                    sb.appendFixP(connection.sf);

                                    sb.appendFixP(point.x);
                                    sb.appendFixP(point.y);
                                }
                                    break;
                                default:
                                    break;
                            }
                            connection = nextNode.nextConnection;
                        }
                    }
                    sb.append('"/>');
                    break;
                }

            }
        }

        sb.append('</svg>');

        return sb.toString();
    }
};


export const transforms = {
    rotate(nodes, step = {angle: 0, center: {x: 0, y: 0}}) {
        nodes = JSON.parse(JSON.stringify(nodes));
        let resNodes = {};
        let node = nodes.start;
        rotatePoint(node.point);
        let connection = node.nextConnection;
        while (connection) {
            let nextNode = connection.nextNode;

            switch (connection.name) {
                case "C": {
                    rotatePoint(connection.point1);
                    rotatePoint(connection.point2);
                    break;
                }
                case "S":
                case "Q": {
                    rotatePoint(connection.point1);
                    break;
                }
                default:
                    break;
            }

            rotatePoint(nextNode.point);

            connection = nextNode.nextConnection;
        }
        resNodes.start = node;
        return resNodes;

        function rotatePoint(p) {
            let x = (p.x ?? 0) - step.center.x;
            let y = (p.y ?? 0) - step.center.y;
            p.x = x * Math.cos(step.angle) - y * Math.sin(r) + step.center.x;
            p.y = x * Math.sin(step.angle) + y * Math.cos(r) + step.center.y;
        }
    }, mirror(nodes, step = {a: 1, b: 1, c: 1}) {
        nodes = JSON.parse(JSON.stringify(nodes));
        let resNodes = {};
        let node = nodes.start;
        mirrorPoint(node.point);
        let connection = node.nextConnection;
        while (connection) {
            let nextNode = connection.nextNode;

            switch (connection.name) {
                case "C": {
                    mirrorPoint(connection.point1);
                    mirrorPoint(connection.point2);
                    break;
                }
                case "S":
                case "Q": {
                    mirrorPoint(connection.point1);
                    break;
                }
                default:
                    break;
            }

            mirrorPoint(nextNode.point);

            connection = nextNode.nextConnection;
        }
        resNodes.start = node;
        return resNodes;

        function mirrorPoint(p) {
            let n = (step.a * p.x + step.b * p.y + step.c) / (step.a * step.a + step.b * step.b);
            p.x = p.x - 2 * step.a * n;
            p.y = p.y - 2 * step.b * n;
        }
    }, reverse(nodes, step = {}) {
        nodes = JSON.parse(JSON.stringify(nodes));
        let resNodes = {};
        let node = nodes.start;
        let connection = node.nextConnection;
        let previewConnection = undefined;
        while (connection) {
            let nextNode = connection.nextNode;

            switch (connection.name) {
                case "C": {
                    [connection.point1, connection.point2] = [connection.point2, connection.point1];
                    break;
                }
                default:
                    break;
            }
            connection.nextNode = node;
            node.nextConnection = previewConnection;
            node = nextNode;
            previewConnection = connection;
            connection = nextNode.nextConnection;
        }
        resNodes.start = node;
        return resNodes;
    }
}

class SvgBuilder {
    _data = [];

    precision = 1;

    constructor(precision = 1) {
        this.precision = precision;
    }


    append(str) {
        this._data.push(str);
    }

    appendFix(num) {
        this.append(parseFloat(num.toFixed(this.precision)));
    }

    appendFixP(num) {
        if (num >= 0) this.append(' ');
        this.append(parseFloat(num.toFixed(this.precision)));
    }

    appendMany(...args) {
        for (const arg of args) {
            if (typeof arg === "number") {
                this.appendFix(arg);
            } else {
                this.append(arg);
            }
        }
    }

    appendJoin(join, ...args) {

        for (let i = 0; i < args.length; i++) {
            if (i !== 0) this.append(join);

            const arg = args[i];
            if (typeof arg === "number") {
                this.appendFix(arg);
            } else {
                this.append(arg);
            }
        }
    }

    toString() {
        return this._data.join("");
    }
}


