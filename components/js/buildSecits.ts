import {FontBuilder} from "../../../dragert/fonts/src/font-builder"
import {FileHelper} from "../../../dragert/utils/src/file-helper";
import * as path from "path";
import {join} from "node:path";
import * as fs from "fs"


const __dirname = path.resolve();
const folder = join(__dirname, '../icons');
console.log("Current dir: " + folder);


let index = 1;
let iconIndex = {
    "fontName": "secits-icons",
    "fonts": {}
};

await buildIcon("solid")
await FileHelper.writeFile(join(folder, "secits-icon.json"), JSON.stringify(iconIndex, null, 2));

async function buildIcon(iconType: string) {
    const className = `.si${iconType[0]},.si-${iconType}`;
    const fileName = `si-${iconType}`;


    const svgPath = join(folder, "svg", iconType);
    const files = fs.readdirSync(svgPath);

    let builder = new FontBuilder();
    files.forEach(file => {
        if (!file.endsWith(".svg")) {
            return;
        }
        const filePath = path.join(svgPath, file);
        const name = file.substring(0, file.length - 4);
        let unicode = "";
        if (iconIndex["fonts"][name]) {
            unicode = iconIndex["fonts"][name];
        } else {
            unicode = String.fromCodePoint(57344 + index++);
            iconIndex["fonts"][name] = unicode;
        }

        builder.addSvg({
            name: name, path: filePath, unicode: unicode
        })
    });

    await builder.buildSvgFontCss(join(folder, "../css/icons", fileName + ".less"), className);
    await builder.buildSvg(join(folder, "svg", fileName + ".svg"), fileName);
    await builder.buildTtfFont(join(folder, "svg", fileName + ".svg"), join(folder, "tff", fileName + ".ttf"))
    await builder.buildWoff2Font(join(folder, "tff", fileName + ".ttf"), join(folder, "woff2", fileName + ".woff2"))
}
